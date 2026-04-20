using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCollectibleItem : MonoBehaviour
{
    public int scoreValue = 10;

    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            Collect();
        }
    }

    private void Collect()
    {
        isCollected = true;

        // 通知收集系统
        FixedCollectibleSystem collectSystem = FindObjectOfType<FixedCollectibleSystem>();
        if (collectSystem != null)
        {
            collectSystem.CollectItem(scoreValue);
        }

        // 隐藏物品
        gameObject.SetActive(false);
    }
}
