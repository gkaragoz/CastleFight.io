using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingItem : MonoBehaviour {

    [SerializeField]
    private Button _btnBuilding;

    [SerializeField]
    private Image _imgThumbnail;

    [SerializeField]
    private TextMeshProUGUI _txtName;

    private Building _building;

    public void Initialize(Building building, BuildingManager.OnClickAnyBuildingItem method) {
        this._building = building;

        SetName(building.GetName());
        SetOnclick(method);
        SetThumbnail(building.GetThumbnail());
    }

    public void SetName(string name) {
        if (string.IsNullOrEmpty(name)) {
            Debug.LogError("No name found.");
            return;
        }

        _txtName.text = name;
    }

    public void SetOnclick(BuildingManager.OnClickAnyBuildingItem method) {
        if (method == null) {
            Debug.LogError("No action method found.");
            return;
        }
        _btnBuilding.onClick.AddListener(delegate { method?.Invoke(this._building); });
    }

    public void SetThumbnail(Sprite thumbnail) {
        if (thumbnail == null) {
            Debug.LogError("Thumbnail sprite not found.");
            return;
        }

        _imgThumbnail.sprite = thumbnail;
    }

}
