using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public delegate void OnClickAnyBuildingItem(Building building);
    OnClickAnyBuildingItem onClickedAnyBuildingItem;

    [SerializeField]
    private List<Building> _buildings = new List<Building>();

    [SerializeField]
    private BuildingItem _buildingItemPrefab;

    [SerializeField]
    private Transform _buildingItemsParent;

    private BuildingPlacer _buildingPlacer;

    private void Awake() {
        _buildingPlacer = GetComponent<BuildingPlacer>();

        onClickedAnyBuildingItem = OnClickedBuildingItem;
    }

    private void Start() {
        InitiailzeBuildingsMenu();
    }

    private void InitiailzeBuildingsMenu() {
        for (int ii = 0; ii < _buildings.Count; ii++) {
            CreateBuildingItem(_buildings[ii]);
        }
    }

    private void CreateBuildingItem(Building building) {
        BuildingItem buildingItem = Instantiate(_buildingItemPrefab, _buildingItemsParent).GetComponent<BuildingItem>();

        buildingItem.Initialize(building, onClickedAnyBuildingItem);
    }

    private void OnClickedBuildingItem(Building building) {
        if (_buildingPlacer.HasBuildingSelected()) {
            return;
        }

        GameObject buildingObj = Instantiate(building.GetPrefab());

        _buildingPlacer.SelectBuilding(buildingObj);
    }

    public void BuildIt() {
        if (_buildingPlacer.CheckCollisions()) {
            Debug.Log("Can't build here!");
        } else {
            _buildingPlacer.ResetIndicators();
        }
    }

    public void CancelIt() {
        _buildingPlacer.ResetIndicators();
    }

}
