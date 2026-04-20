using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BagStore", menuName = "BagStore/New BagStore")]
public class BagStore :ScriptableObject
{
    public List<Item> itemList = new List<Item>();

}
