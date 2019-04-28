using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacer : MonoBehaviour {

    [SerializeField]
    private LayerMask _structureLayerMask;
    [SerializeField]
    private LayerMask _buildAreaLayerMask;
    [SerializeField]
    private Transform _meshParent;
    [SerializeField]
    private GameObject _container;
    [SerializeField]
    private Vector3 _truePosition;
    [SerializeField]
    private float _gridSize = 0.5f;
    [SerializeField]
    private Building _selectedBuilding;
    [SerializeField]
    private BuildingIndicator _buildingIndicator;
    [SerializeField]
    private CameraController CameraController;

    public Building SelectedBuilding { get { return _selectedBuilding; } }

    private void Awake() {
        _buildingIndicator.onBeginDrag = OnBeginDrag;
        _buildingIndicator.onDrag = OnDrag;
        _buildingIndicator.onEndDrag = OnEndDrag;
    }

    private void OnBeginDrag(PointerEventData data) {
        if (HasBuildingSelected()) {
            CameraController.IsLocked = true;
            CameraController.ResetTarget();
        }
    }

    private void OnDrag(PointerEventData data) {
        if (HasBuildingSelected()) {
            CameraController.IsLocked = true;
            CameraController.ResetTarget();

            _container.transform.position = data.pointerCurrentRaycast.worldPosition;

            _truePosition.x = Mathf.Floor(_container.transform.position.x / _gridSize) * _gridSize;
            _truePosition.y = Mathf.Floor(_container.transform.position.y / _gridSize) * _gridSize;
            _truePosition.z = Mathf.Floor(_container.transform.position.z / _gridSize) * _gridSize;

            _meshParent.position = _truePosition;
            MoveHandlerUIPosition(_truePosition);
        }
    }

    private void OnEndDrag(PointerEventData data) {
        if (HasBuildingSelected()) {
            CameraController.IsLocked = false;
            CameraController.SetTarget(_selectedBuilding.transform);
        }
    }

    private void MoveHandlerUIPosition(Vector3 position) {
        _buildingIndicator.transform.parent.position = new Vector3(position.x, _buildingIndicator.transform.parent.position.y, position.z);
        _buildingIndicator.SetColor(IsAvailableToBuild());
        _buildingIndicator.Show();
    }

    public bool HasBuildingSelected() {
        return (_selectedBuilding == null) ? false : true;
    }

    public void ResetIndicators(bool shouldDestroyBuilding) {
        if (HasBuildingSelected()) {
            _selectedBuilding.transform.SetParent(null);
            _selectedBuilding = null;
        }

        _truePosition = Vector3.zero;
        _container.transform.position = Vector3.zero;
        _meshParent.transform.position = Vector3.zero;
        _buildingIndicator.Hide();

        CameraController.ResetTarget();
    }

    public bool IsAvailableToBuild() {
        if (_selectedBuilding == null) {
            Debug.LogError("Selected building not found.");
            return false;
        }

        return (_selectedBuilding.IsCollidingToAnotherStructure(_structureLayerMask) == false && _selectedBuilding.IsInBuildArea(_buildAreaLayerMask) == true) ? true : false;
    }

    public void SelectBuilding(GameObject buildingObject) {
        if (HasBuildingSelected()) {
            return;
        }

        if (buildingObject == null) {
            Debug.LogError("Building prefab not found.");
            return;
        }

        _selectedBuilding = buildingObject.GetComponent<Building>();
        _selectedBuilding.transform.SetParent(_meshParent);
        _selectedBuilding.transform.position = Vector3.zero;
        MoveHandlerUIPosition(_selectedBuilding.transform.position);

        CameraController.SetTarget(_selectedBuilding.transform);
    }

}
