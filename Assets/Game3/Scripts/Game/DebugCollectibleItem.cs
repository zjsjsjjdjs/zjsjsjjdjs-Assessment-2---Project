using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCollectibleItem : MonoBehaviour
{
    public int scoreValue = 10;

    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            Debug.Log($"[收集物品] 玩家进入触发器: {other.name}");
            Collect();
        }
        else if (!other.CompareTag("Player"))
        {
            Debug.Log($"[收集物品] 非玩家进入触发器: {other.name}, Tag: {other.tag}");
        }
        else if (isCollected)
        {
            Debug.Log($"[收集物品] 物品已被收集");
        }
    }

    private void Collect()
    {
        isCollected = true;
        Debug.Log($"[收集物品] 开始收集，分数值: {scoreValue}");

        // 通知收集系统
        DebugCollectibleSystem collectSystem = FindObjectOfType<DebugCollectibleSystem>();
        if (collectSystem != null)
        {
            Debug.Log("[收集物品] 找到收集系统");
            collectSystem.CollectItem(scoreValue);
        }
        else
        {
            Debug.LogError("[收集物品] 未找到收集系统！");
        }

        // 隐藏物品
        gameObject.SetActive(false);
        Debug.Log("[收集物品] 物品已隐藏");
    }
}
