using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingIndicator : Menu, IDraggable, IClickable {

    [SerializeField]
    private Color _readyColor;
    [SerializeField]
    private Color _notReadyColor;
    [SerializeField]
    private Image _imgInteraction;

    public Action<PointerEventData> onBeginDrag;
    public Action<PointerEventData> onDrag;
    public Action<PointerEventData> onEndDrag;

    public Action<PointerEventData> onPointerDown;
    public Action<PointerEventData> onPointerUp;
    public Action<PointerEventData> onPointerClick;

    public void OnBeginDrag(PointerEventData data) {
        onBeginDrag?.Invoke(data);
    }

    public void OnDrag(PointerEventData data) {
        onDrag?.Invoke(data);
    }

    public void OnEndDrag(PointerEventData data) {
        onEndDrag?.Invoke(data);
    }

    public void OnPointerDown(PointerEventData data) {
        onPointerDown?.Invoke(data);
    }

    public void OnPointerUp(PointerEventData data) {
        onPointerUp?.Invoke(data);
    }

    public void OnPointerClick(PointerEventData data) {
        onPointerClick?.Invoke(data);
    }

    public void SetColor(bool isOkay) {
        if (isOkay) {
            _imgInteraction.color = _readyColor;
        } else {
            _imgInteraction.color = _notReadyColor;
        }
    }
}
