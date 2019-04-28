using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private Building_SO _buildingDefinition_Template;
    [SerializeField]
    private GameObject _mainBuilding;
    [SerializeField]
    private GameObject[] _constructions;

    [Header("Debug")]
    [SerializeField]
    private Building_SO _building;
    [SerializeField]
    private bool _isCollidingToAnotherStructure;

    #region Initializations

    private void Awake() {
        if (_buildingDefinition_Template != null) {
            _building = Instantiate(_buildingDefinition_Template);
        }
    }

    #endregion

    #region Increasers

    public void IncreaseHealth(float value) {
        if (GetCurrentHealth() + value >= GetMaxHealth()) {
            return;
        }

        _building.CurrentHealth += value;
    }

    #endregion

    #region Decreasers

    public void DecreaseHealth(float value) {
        _building.CurrentHealth -= value;

        if (GetCurrentHealth() <= 0) {
            _building.CurrentHealth = 0;
        }
    }

    #endregion

    #region Setters

    public void SetIsBuilding(bool status) {
        _building.IsBuilding = status;
    }

    public void SetCurrentHealth(float amount) {
        if (amount <= 0) {
            _building.CurrentHealth = 0;
            return;
        }
        if (amount > GetMaxHealth()) {
            _building.MaxHealth = amount;
        }

        _building.CurrentHealth = amount;
    }

    #endregion

    #region Reporters

    public bool IsCollidingToAnotherStructure(LayerMask structureLayerMask) {
        return CheckCollisions(structureLayerMask);
    }

    public string GetName() {
        return _buildingDefinition_Template.Name;
    }

    public GameObject GetPrefab() {
        return _buildingDefinition_Template.Prefab;
    }

    public Sprite GetThumbnail() {
        return _buildingDefinition_Template.Thumbnail;
    }

    public float GetBuildingTime() {
        return _building.BuildingTime;
    }

    public float GetPrice() {
        return _building.Price;
    }

    public bool GetIsBuilding() {
        return _building.IsBuilding;
    }

    public float GetCurrentHealth() {
        return _building.CurrentHealth;
    }

    public float GetMaxHealth() {
        return _building.MaxHealth;
    }

    #endregion

    #region Custom Methods

    private float GetConstructionPartCooldown() {
        return GetBuildingTime() / (_constructions.Length);
    }

    private void ShowMainBuilding() {
        _mainBuilding.SetActive(true);
    }

    private void HideMainBuilding() {
        _mainBuilding.SetActive(false);
    }

    private void ShowConstructionPrefab(int index) {
        if (index > _constructions.Length - 1 || index < 0) {
            Debug.LogError("Index out of bounds.");
            return;
        }

        _constructions[index].SetActive(true);
    }

    private void HideConstructionPrefab(int index) {
        if (index > _constructions.Length - 1 || index < 0) {
            Debug.LogError("Index out of bounds.");
            return;
        }

        _constructions[index].SetActive(false);
    }

    private IEnumerator IBuildingProcess() {
        Debug.Log("...Building: " + GetName());

        float processedTime = 0;
        float partCooldown = GetConstructionPartCooldown();
        float upgradeTimestamep = 0;
        int constructionPrefabIndex = -1;

        HideMainBuilding();

        while (true) {
            processedTime += Time.deltaTime;

            // If building completed, exit loop.
            if (processedTime >= GetBuildingTime()) {
                break;
            }

            // Checkpoint for new construction prefab.
            if (Time.time > upgradeTimestamep) {
                upgradeTimestamep = Time.time + partCooldown;

                // Hide previous construction prefab.
                if (constructionPrefabIndex >= 0) {
                    HideConstructionPrefab(constructionPrefabIndex);
                }

                // Show next construction prefab.
                constructionPrefabIndex++;
                ShowConstructionPrefab(constructionPrefabIndex);
            }

            SetCurrentHealth(processedTime.Map(0, GetBuildingTime(), 0, GetMaxHealth()));

            yield return new WaitForFixedUpdate();
        }

        Debug.Log("Build completed: " + GetName());
        ShowMainBuilding();
        SetIsBuilding(false);

        yield break;
    }

    public bool CheckCollisions(LayerMask structureLayerMask) {
        Collider[] colliders = Physics.OverlapBox(transform.position, GetComponent<BoxCollider>().size * 0.5f, Quaternion.identity, structureLayerMask);
        for (int ii = 0; ii < colliders.Length; ii++) {
            if (colliders[ii].transform == this.transform) {
                continue;
            } else {
                return true;
            }
        }
        return false;
    }

    public void StartBuilding() {
        SetIsBuilding(true);

        StartCoroutine(IBuildingProcess());
    }

    #endregion

}
