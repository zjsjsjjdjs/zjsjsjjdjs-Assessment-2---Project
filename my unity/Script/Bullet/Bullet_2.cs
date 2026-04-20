using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色使用的子弹 负责子弹的移动 子弹的碰撞检测
/// </summary>
public class Bullet_2 : MonoBehaviour
{
    public LayerMask DetectionLayer; // 子弹检测的物理层级
    
    public float LifeTime; // 子弹存活时间
    public float DetectionR; // 子弹检测半径
    public float moveSpeed; // 子弹移动
    public float Damage; // 子弹伤害

    [HideInInspector]
    public float ActiveTime; // 子弹激活时间
    private Vector3 LastPos; // 子弹上一帧位置
 
    public PlayerControl PlayerControl;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
  
        Vector3 movedir = transform.forward;
        movedir.y = 0;
        movedir.Normalize();
        LastPos = transform.position;
        transform.position += movedir * moveSpeed * Time.deltaTime; // 移动
        
        if (Time.time - ActiveTime >= LifeTime)
        {
            Stop();
        }
        else
        {
            Vector3 dir = transform.position - LastPos;
            RaycastHit hit;
            // 球形射线检测
            if (Physics.SphereCast(LastPos, DetectionR, dir, out hit, dir.magnitude,DetectionLayer))
            {
                // 播放子弹碰撞后的粒子特效
                GameObject explosionObj = PlayerControl.GetBulletExplosionPrefabObject();
                explosionObj.transform.position = hit.point;
                explosionObj.SetActive(true);
                // explosionObj.GetComponent<ParticleSystem>().Play();
                Debug.Log("aaa");
                MonsterControl monsterControl = hit.transform.GetComponent<MonsterControl>();
                if (monsterControl) // 如果碰撞到敌人 对敌人造成伤害
                {
                    monsterControl.Hit(Damage);
                }else if (hit.transform.tag.Equals("MonsterBullet")) // 如果碰到敌人发射的子弹 销毁敌人子弹
                {
                    MonsterBullet monsterBullet = hit.transform.GetComponent<MonsterBullet>();
                    monsterBullet.Stop(true);
                }

                Stop();
            }
            
        }

    }

    private void Stop()
    {
        // GetComponent<ParticleSystem>().Stop();
        PlayerControl.SetBulletObject(gameObject); // 放回对象池中
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, DetectionR);
    }
}
