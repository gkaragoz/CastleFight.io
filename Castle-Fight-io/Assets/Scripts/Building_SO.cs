using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Building")]
public class Building_SO : ScriptableObject {

    #region Properties

    [SerializeField]
    private string _name = "Building";


    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private Image _thumbnailImage;

    public string Name {
        get { return _name; }
        private set { _name = value; }
    }

    public GameObject Prefab {
        get { return _prefab; }
        private set { _prefab = value; }
    }

    public Image ThumbnailImage {
        get { return _thumbnailImage; }
        private set { _thumbnailImage = value; }
    }

    #endregion

}