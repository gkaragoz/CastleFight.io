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

    public string Name {
        get { return _name; }
        private set { _name = value; }
    }

    public GameObject Prefab {
        get { return _prefab; }
        private set { _prefab = value; }
    }

    public Sprite Thumbnail {
        get { return _thumbnail; }
        private set { _thumbnail = value; }
    }

    #endregion

}