using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;
    public BagStore playerBag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject,0.05f);
        }
    }

    private void AddNewItem()
    {
        if (! playerBag.itemList.Contains(thisItem))//判断是否背包中有该物体 无建立列表项目
        {
            for (int i = 0; i < playerBag.itemList.Count; i++)
            {
                if (playerBag.itemList[i]==null)
                {
                    playerBag.itemList[i] = thisItem;
                    thisItem.itemHeld = 1;
                    break;
                }
            }
        }
        else  //判断是否背包中有该物体 有+1
        {
            thisItem.itemHeld += 1;
        }
        BagManager.RefreshItem();
    }
}
