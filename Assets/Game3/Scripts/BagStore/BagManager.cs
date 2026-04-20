using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagManager : MonoBehaviour
{
    static BagManager instance;
    public BagStore myBag;
    public GameObject slotGrid;
    public GameObject emptySlot;
    public Text itemInformation;
    public List<GameObject> slots = new List<GameObject>();
    private void Awake()
    {
        if (instance!=null)
        {
            Destroy(this);
        }
        instance = this;
    }
    private void OnEnable()
    {
        RefreshItem();
        instance.itemInformation.text = "";
    }

    public static void UpdateInfo(string itemDes)
    {
        instance.itemInformation.text = itemDes;
    }


    public static void RefreshItem()
    {
        //循环删除slotGrid下的子物体
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount==0)
               break;           
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }


        //重新生成对应的物品slot
        for (int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            instance.slots.Add(Instantiate(instance.emptySlot));
            instance.slots[i].transform.SetParent(instance.slotGrid.transform, false);//使生成的Item成为Grid的子物体,false是 是否为世界坐标 默认true会变大很多
            instance.slots[i].GetComponent<Slot>().slotID = i;
            instance.slots[i].GetComponent<Slot>().setUpSlot(instance.myBag.itemList[i]);
        }
    }
}
