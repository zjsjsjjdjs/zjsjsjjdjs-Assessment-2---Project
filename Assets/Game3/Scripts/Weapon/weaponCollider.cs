using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponCollider : MonoBehaviour
{
    
    public GameObject weapon;
    public int weaponDamage=25;
    private void Awake()
    {       
      weapon.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        PlayerState playerState = other.GetComponent<PlayerState>();
        if (playerState != null)
        {
            playerState.TackDamage(weaponDamage);
            Debug.Log("武器打中玩家HP-25");
        }


        EnemyState enemyState = other.GetComponent<EnemyState>();

        if (enemyState != null)
        {
           
         Debug.Log("武器打中敌人 HP-25");
         enemyState.TackDamage(weaponDamage);

            
        }
    }
 
    
}
