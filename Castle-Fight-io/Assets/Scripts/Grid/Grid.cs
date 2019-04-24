using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class Grid : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private TextMeshProUGUI _txtIndex;
    [SerializeField]
    private Material _greenMaterial;
    [SerializeField]
    private Material _normalMaterial;
    [SerializeField]
    private Material _redMaterial;

    [Header("Debug")]
    [SerializeField]
    private bool _isObstacle;
    [SerializeField]
    private bool _hasInvaded;
    [SerializeField]
    private Vector2 _matrix;
    [SerializeField]
    private Vector2 _size;
    [SerializeField]
    private Vector3 _position;

    public Vector2 Matrix { get => _matrix; set => _matrix = value; }
    public Vector2 Size { get => _size; set => _size = value; }
    public Vector3 Position { get => _position; set => _position = value; }
    public bool HasInvaded { get => _hasInvaded; set => _hasInvaded = value; }
    public bool IsObstacle { get => _isObstacle; set => _isObstacle = value; }
    public List<Grid> Neighbours { get => _neighbours; set => _neighbours = value; }

    private List<Grid> _neighbours = new List<Grid>();
    private Coroutine _painterCoroutine;

    private void Start() {
        _txtIndex.text = _matrix.x.ToString("0") + "," + _matrix.y.ToString("0");
        gameObject.name = _txtIndex.text;

        if (Random.Range(0, 100) > 98) {
            _isObstacle = true;
        }

        if (_isObstacle) {
            PaintObstacle();
        }
    }

    public void Paint() {
        Renderer rend = GetComponent<Renderer>();
        rend.material = _greenMaterial;

        HasInvaded = true;

        if (_painterCoroutine == null && !_isObstacle) {
            _painterCoroutine = StartCoroutine(Depaint());
        }
    }

    public IEnumerator Depaint() {
        yield return new WaitForSeconds(0.3f);

        Renderer rend = GetComponent<Renderer>();
        rend.material = _normalMaterial;

        HasInvaded = false;

        _painterCoroutine = null;
    }

    public void PaintObstacle() {
        Renderer rend = GetComponent<Renderer>();
        rend.material = _redMaterial;

        HasInvaded = true;
    }

}
