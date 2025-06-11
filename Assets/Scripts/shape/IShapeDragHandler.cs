using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IShapeDragHandler 
{
    void OnPointerDown(PointerEventData eventData);
    void OnBeginDrag(PointerEventData eventData);
    void OnDrag(PointerEventData eventData, RectTransform transform, Vector2 offset, Canvas canvas);
    void OnEndDrag(PointerEventData eventData, RectTransform transform, Vector3 originalScale);
}
