using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppableItem : MonoBehaviour, IDropHandler
{
    GameManager GM;
    DragableItem dragItem;

    //public static int noOfBoxesFilled = 0;

    private void Awake()
    {
        GM = GameObject.FindObjectOfType<GameManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            dragItem = eventData.pointerDrag.gameObject.GetComponent<DragableItem>();
            dragItem.previousParent = transform;

            eventData.pointerDrag.gameObject.transform.position = transform.position;
            //noOfBoxesFilled++;
            //Debug.Log("No of Droppables Filled : " + noOfBoxesFilled);

            if(transform.GetSiblingIndex() == 0)
            {
                GM.finalV1 = dragItem.value;
            }
            else if(transform.GetSiblingIndex() == 2)
            {
                GM.finalV2 = dragItem.value;
            }
            else if(transform.GetSiblingIndex() == 4)
            {
                GM.finalResult = dragItem.value;
            }

            GM.CheckForLevelComplete();
            GM.CheckForLevelFail();
        }
    }
}