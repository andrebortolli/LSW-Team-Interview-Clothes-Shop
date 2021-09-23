using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


[RequireComponent(typeof(CanvasGroup))]
public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IInitializePotentialDragHandler
{
    private RectTransform myRectTransform;
    private Canvas parentCanvas;
    private CanvasGroup myCanvasGroup;

    [SerializeField] private bool useDragThreshold = false;

    public Canvas ParentCanvas { get => parentCanvas; set => parentCanvas = value; }
    public RectTransform MyRectTransform { get => myRectTransform; set => myRectTransform = value; }
    public CanvasGroup MyCanvasGroup { get => myCanvasGroup; set => myCanvasGroup = value; }

    private void Awake()
    {
        MyRectTransform = GetComponent<RectTransform>();
        ParentCanvas = transform.GetComponentInParent<Canvas>();
        MyCanvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        MyCanvasGroup.blocksRaycasts = false;
        myCanvasGroup.alpha = 0.7f;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if(eventData.dragging)
        {
            MyRectTransform.anchoredPosition += eventData.delta / ParentCanvas.scaleFactor ;
        }
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        MyCanvasGroup.blocksRaycasts = true;
        myCanvasGroup.alpha = 1.0f;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {

    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {

    }

    public virtual void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = useDragThreshold;
    }
}
