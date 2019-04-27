using UnityEngine.EventSystems;

public interface IDraggable : IBeginDragHandler, IDragHandler, IEndDragHandler {

    new void OnBeginDrag(PointerEventData data);

    new void OnDrag(PointerEventData data);

    new void OnEndDrag(PointerEventData data);

}
