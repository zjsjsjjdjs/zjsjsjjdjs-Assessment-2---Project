using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponTriger : MonoBehaviour
{
   public GameObject weapon;
   
   
   public void weaponBox()
    {
        weapon.GetComponent<BoxCollider>().enabled = false;
    }
   
}
