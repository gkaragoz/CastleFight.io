using UnityEngine;

public class SnappedMove : MonoBehaviour {

    [SerializeField]
    private GameObject _container;
    [SerializeField]
    private GameObject _structureMesh;
    [SerializeField]
    private LayerMask _groundMask;//select the layer of your ground here, or just choose Default.

    [SerializeField]
    private Vector3 _truePosition;
    [SerializeField]
    private float _gridSize = 0.5f;
    [SerializeField]
    private bool _isPlacing = false;

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)) {
            if (_isPlacing) {
                _isPlacing = false;
                return;
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundMask)) {
                if (hit.collider.tag == "Structure") {
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

    private void LateUpdate() {
        _truePosition.x = Mathf.Floor(_container.transform.position.x / _gridSize) * _gridSize;
        _truePosition.y = Mathf.Floor(_container.transform.position.y / _gridSize) * _gridSize;
        _truePosition.z = Mathf.Floor(_container.transform.position.z / _gridSize) * _gridSize;

        _structureMesh.transform.position = _truePosition;
    }

}
