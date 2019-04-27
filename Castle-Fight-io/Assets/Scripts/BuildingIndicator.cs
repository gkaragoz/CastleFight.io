using UnityEngine;
using UnityEngine.UI;

public class BuildingIndicator : DraggableUI {

    [SerializeField]
    private Color _readyColor;
    [SerializeField]
    private Color _notReadyColor;
    [SerializeField]
    private Image _imgInteraction;

    public void SetColor(bool isColliding) {
        if (isColliding) {
            _imgInteraction.color = _notReadyColor;
        } else {
            _imgInteraction.color = _readyColor;
        }
    }
    
}
