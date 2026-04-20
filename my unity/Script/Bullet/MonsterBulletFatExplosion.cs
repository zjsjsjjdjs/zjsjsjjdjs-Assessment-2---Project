using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletFatExplosion : MonoBehaviour
{
    public float LifeTime; // 特效存活时间

    [HideInInspector]
    public float ActiveTime; // 激活时间

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - ActiveTime >= LifeTime)
        {
            GameManager.Instance.MonsterBulletExpPool.Release(gameObject);
        }
    }
}
