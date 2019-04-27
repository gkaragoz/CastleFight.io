using UnityEngine;

public class Building : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private Building_SO _buildingDefinition_Template;

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

    #endregion

    #region Custom Methods

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

    #endregion

}
