using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoyStickAttack : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform background;
    public RectTransform handle;

    private float joyStickRadius;
    
    public Vector2 Input { get; private set; }

    private void Start()
    {
        joyStickRadius = background.rect.width * 0.5f;
    }

    private void Update()
    {
        Debug.Log(Input);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                background, eventData.position, eventData.pressEventCamera, out Vector2 position))
        {
            position = Vector2.ClampMagnitude(position, joyStickRadius);
            handle.anchoredPosition = position;
            Input = position.normalized;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}