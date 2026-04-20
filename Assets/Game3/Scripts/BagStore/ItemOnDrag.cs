using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;//鼠标点击为事件

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform oriParent;
    public BagStore myBag;
    private int currentItemID;//当前物品ID
    public void OnBeginDrag(PointerEventData eventData)
    {

        oriParent = transform.parent;
        currentItemID = oriParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)//防止在世界地图外 卡住的问题
        {


            if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage")//检测是否有物品  有物体的情况
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent); //物体换位 换父物体
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;//储存位置的改变

                var temp = myBag.itemList[currentItemID];
                myBag.itemList[currentItemID] = myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
                //鼠标点击物品 找到对应的ID
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;

                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = oriParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(oriParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;//鼠标射线检测
                return;
            }


            if (eventData.pointerCurrentRaycast.gameObject.name == "itemButton(Clone)")//防止物品放在格子外
            {

                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;//储存位置的改变


                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = myBag.itemList[currentItemID];
                //  if (eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID != currentItemID)//移动物体放回原位 会消失的bug
                //  {
                // myBag.itemList[currentItemID] = null;
                // }
                GetComponent<CanvasGroup>().blocksRaycasts = true;//鼠标射线检测
                return;
            }

        }

            //其他任何位置都归位
            transform.SetParent(oriParent);
            transform.position = oriParent.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
