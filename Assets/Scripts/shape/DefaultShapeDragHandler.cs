using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultShapeDragHandler : IShapeDragHandler
{
    public void OnPointerDown(PointerEventData eventData) { }

    public void OnBeginDrag(PointerEventData eventData) { }

    public void OnDrag(PointerEventData eventData, RectTransform transform, Vector2 offset, Canvas canvas)
    {
        transform.anchorMin = Vector2.zero;
        transform.anchorMax = Vector2.zero;
        transform.pivot = Vector2.zero;
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, eventData.position, Camera.main, out pos);
        transform.localPosition = pos + offset;
    }

    public void OnEndDrag(PointerEventData eventData, RectTransform transform, Vector3 originalScale)
    {
        transform.localScale = originalScale;
        GameEvents.CheckIfShapeCanBePlaced();
    }
}
