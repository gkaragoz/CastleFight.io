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

    #endregion

}