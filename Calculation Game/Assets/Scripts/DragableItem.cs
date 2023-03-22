using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform previousParent;
    public Transform newParent;

    [HideInInspector] public int value;

    Text valueTextBox;
    Vector3 startPosition;

    private void OnEnable()
    {
        valueTextBox = transform.GetChild(0).gameObject.GetComponent<Text>();
        startPosition = transform.position;
    }

    public void ResetPositions()
    {
        transform.position = startPosition;
    }

    public void SetValueTextBox(int value)
    {
        this.value = value;
        if (valueTextBox)
        {
            valueTextBox.text = value.ToString();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Beginning of Drag");
        previousParent = transform.parent;
        GetComponent<Image>().raycastTarget = false;

        //if(transform.parent.gameObject.GetComponent<DroppableItem>() != null)
        //{
        //    DroppableItem.noOfBoxesFilled--;
        //}
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        transform.SetParent(newParent);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Ending of Drag");
        transform.SetParent(previousParent);
        GetComponent<Image>().raycastTarget = true;
    }
}
