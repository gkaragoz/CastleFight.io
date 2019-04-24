using UnityEngine;

public class BuildingPlacer : MonoBehaviour {

    [SerializeField]
    private GameObject[] _buildings;
    [SerializeField]
    private Transform _meshParent;

    private SnappedMove _snappedMove;

    private void Start() {
        _snappedMove = GetComponent<SnappedMove>();
    }

    public void SelectBuilding(int index) {
        if (_snappedMove.HasMesh()) {
            return;
        }

        if (index < 0 || index > _buildings.Length - 1) {
            Debug.LogError("Index out of bound.");
            return;
        }

        Transform buildingObj = Instantiate(_buildings[index], _meshParent).transform;
        _snappedMove.SetMesh(buildingObj);
    }

}
