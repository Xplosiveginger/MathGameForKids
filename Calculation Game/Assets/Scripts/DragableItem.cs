using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform previousParent;
    [HideInInspector] public int value;
    Text valueTextBox;
    Vector3 startPosition;

    private void OnEnable()
    {
        valueTextBox = transform.GetChild(0).gameObject.GetComponent<Text>();
        startPosition = transform.position;
    }

    public void SetValueTextBox(int value)
    {
        if(valueTextBox)
        {
            this.value = value;
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
        transform.parent = transform.root;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Ending of Drag");
        transform.parent = previousParent;
        GetComponent<Image>().raycastTarget = true;
    }
}
