using UnityEngine;

public class SnappedMove : MonoBehaviour {

    [SerializeField]
    private GameObject _container;
    [SerializeField]
    private GameObject _buildingPrefab;

    private Vector3 _truePosition;
    private float _gridSize = 0.5f;

    private void LateUpdate() {
        _truePosition.x = Mathf.Floor(_container.transform.position.x / _gridSize) * _gridSize;
        _truePosition.y = Mathf.Floor(_container.transform.position.y / _gridSize) * _gridSize;
        _truePosition.z = Mathf.Floor(_container.transform.position.z / _gridSize) * _gridSize;

        _buildingPrefab.transform.position = _truePosition;
    }

}
