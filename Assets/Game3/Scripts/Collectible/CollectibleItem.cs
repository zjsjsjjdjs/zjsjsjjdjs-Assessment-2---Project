using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [Header("Collectible Settings")]
    public int scoreValue = 10;
    public string collectSound = "UI点击";
    public GameObject collectEffect;

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

        // 播放收集音效
        if (!string.IsNullOrEmpty(collectSound))
        {
            try
            {
                GameFacade.Instance.PlaySound(collectSound);
            }
            catch (System.Exception)
            {
                // 如果音效不存在，不报错
            }
        }

        // 生成收集特效
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }

        // 通知管理器
        CollectibleManager.Instance.CollectItem(this);

        // 隐藏物品
        gameObject.SetActive(false);
    }
}
