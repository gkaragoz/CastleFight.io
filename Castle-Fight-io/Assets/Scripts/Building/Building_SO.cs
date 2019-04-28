using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Building")]
public class Building_SO : ScriptableObject {

    #region Properties

    [SerializeField]
    private string _name = "Building";

    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private Sprite _thumbnail;

    [SerializeField]
    private float _buildingTime;

    [SerializeField]
    private float _price;

    [SerializeField]
    private bool _isBuilding;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public GameObject Prefab {
        get { return _prefab; }
        set { _prefab = value; }
    }

    public Sprite Thumbnail {
        get { return _thumbnail; }
        set { _thumbnail = value; }
    }

    public float BuildingTime {
        get { return _buildingTime; }
        set { _buildingTime = value; }
    }

    public float Price {
        get { return _price; }
        set { _price = value; }
    }

    public bool IsBuilding {
        get { return _isBuilding; }
        set { _isBuilding = value; }
    }

    #endregion

}