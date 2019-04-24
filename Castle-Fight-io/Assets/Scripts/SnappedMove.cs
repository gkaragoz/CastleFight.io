using UnityEngine;

public class SnappedMove : MonoBehaviour {

    [SerializeField]
    private GameObject _container;
    [SerializeField]
    private Transform _meshParent;
    [SerializeField]
    private LayerMask _groundMask;

    [SerializeField]
    private Vector3 _truePosition;
    [SerializeField]
    private float _gridSize = 0.5f;
    [SerializeField]
    private bool _isPlacing = false;

    public void SetMesh(Transform buildingObj) {
        buildingObj.SetParent(_meshParent);

        _isPlacing = true;
    }

    public bool HasMesh() {
        return (_meshParent.childCount > 0) ? true : false;
    }

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)) {
            if (_isPlacing) {
                PlaceIt();
                return;
            }
            if (!HasMesh()) {
                return;
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                if (hit.collider.transform == _meshParent.GetChild(0)) {
                    _isPlacing = true;
                } else {
                    _isPlacing = false;
                }
            } else {
                _isPlacing = false;
            }
        }

        if (_isPlacing) {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundMask)) {
                if (hit.collider.tag == "Ground") {
                    _container.transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
                }
            } else {
                _isPlacing = false;
            }
        }
    }

    private void PlaceIt() {
        _isPlacing = false;

        if (HasMesh()) {
            _meshParent.GetChild(0).SetParent(null);
        }
    }

    private void LateUpdate() {
        if (_meshParent.childCount > 0) {
            _truePosition.x = Mathf.Floor(_container.transform.position.x / _gridSize) * _gridSize;
            _truePosition.y = Mathf.Floor(_container.transform.position.y / _gridSize) * _gridSize;
            _truePosition.z = Mathf.Floor(_container.transform.position.z / _gridSize) * _gridSize;

            _meshParent.position = _truePosition;
        }
    }

}
