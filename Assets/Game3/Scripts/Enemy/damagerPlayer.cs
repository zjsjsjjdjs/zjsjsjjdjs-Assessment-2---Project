using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagerPlayer : MonoBehaviour
{
    public int damager = 20;
    private void OnTriggerEnter(Collider other)
    {
        PlayerState playerState = other.GetComponent<PlayerState>();
        if (playerState!=null)
        {
            playerState.TackDamage(damager);
            Debug.Log("PLAYER HP-20");
        }
        EnemyState enemyState = other.GetComponent<EnemyState>();
        if (enemyState!=null)
        {
            enemyState.TackDamage(damager);
            Debug.Log("ENEMY HP-20");
        }
    }
}
