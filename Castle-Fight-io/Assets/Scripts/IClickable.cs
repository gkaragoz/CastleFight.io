using UnityEngine.EventSystems;

public interface IClickable : IPointerDownHandler, IPointerUpHandler, IPointerClickHandler {

    new void OnPointerDown(PointerEventData data);

    new void OnPointerUp(PointerEventData data);

    new void OnPointerClick(PointerEventData data);

}
