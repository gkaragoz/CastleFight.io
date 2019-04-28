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

    #endregion

    #region Decreasers

    #endregion

    #region Setters

    public void SetIsBuilding(bool status) {
        _buildingDefinition_Template.IsBuilding = status;
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
        return _buildingDefinition_Template.BuildingTime;
    }

    public float GetPrice() {
        return _buildingDefinition_Template.Price;
    }

    public bool GetIsBuilding() {
        return _buildingDefinition_Template.IsBuilding;
    }

    #endregion

    #region Custom Methods

    private float GetConstructionPartTime() {
        return GetBuildingTime() / (_constructions.Length + 1); // +1 is main building.
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
        float startedTimeStamp = Time.time;
        float constructionTimer = 0;
        float constructionPartTime = GetConstructionPartTime();
        int constructionPrefabIndex = -1;

        HideMainBuilding();

        while (true) {
            // Checkpoint for new construction prefab.
            if (Time.time > constructionTimer) {
                constructionTimer = Time.time + constructionPartTime;

                // Hide previous construction prefab.
                if (constructionPrefabIndex >= 0) {
                    HideConstructionPrefab(constructionPrefabIndex);
                }

                // If building completed, exit loop.
                if (constructionTimer - startedTimeStamp >= GetBuildingTime()) {
                    break;
                }

                // Show next construction prefab.
                constructionPrefabIndex++;
                ShowConstructionPrefab(constructionPrefabIndex);
            }

            yield return new WaitForFixedUpdate();
        }

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
