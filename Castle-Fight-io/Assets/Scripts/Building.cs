using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private Building_SO _buildingDefinition_Template;

    [Header("Debug")]
    [SerializeField]
    private Building_SO _building;

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

    public string GetName() {
        return _building.Name;
    }

    public GameObject GetPrefab() {
        return _building.Prefab;
    }

    public Image GetThumbnailImage() {
        return _building.ThumbnailImage;
    }

    #endregion

    #region Custom Methods

    #endregion

}
