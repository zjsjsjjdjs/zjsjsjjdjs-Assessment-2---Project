using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject weapon;
    public int weaponDamage =2;
    public Animator animator;
    private void Update()
    {
        if (animator.GetBool("isDead"))
        {
            weapon.GetComponent<BoxCollider>().enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            PlayerState playerState = other.GetComponent<PlayerState>();
            if (playerState != null)
            {
                playerState.TackDamage(weaponDamage);
                Debug.Log("武器打中玩家HP-2");
            }
        }
        if (other.tag =="shield")
        {
            GameFacade.Instance.PlaySound("格挡");
            Debug.Log("格挡成功");
        }
    }
}
