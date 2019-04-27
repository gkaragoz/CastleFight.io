using System;
using UnityEngine.EventSystems;

public class DraggableUI : Menu, IDraggable {

    public Action<PointerEventData> onBeginDrag;
    public Action<PointerEventData> onDrag;
    public Action<PointerEventData> onEndDrag;

    public void OnBeginDrag(PointerEventData data) {
        onBeginDrag?.Invoke(data);
    }

    public void OnDrag(PointerEventData data) {
        onDrag?.Invoke(data);
    }

    public void OnEndDrag(PointerEventData data) {
        onEndDrag?.Invoke(data);
    }

}
