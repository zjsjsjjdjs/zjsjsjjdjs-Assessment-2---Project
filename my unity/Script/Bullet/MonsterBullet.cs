using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public LayerMask DetectionLayer; // 子弹检测的物理层级
    
    public float LifeTime; // 子弹存活时间
    public float DetectionR; // 子弹检测半径
    public float moveSpeed; // 子弹移动速度
    public float Damage; // 子弹伤害

    [HideInInspector]
    public float ActiveTime; // 子弹激活的时间
    private Vector3 LastPos; // 子弹上一帧位置

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
                GameObject explosionObj = GameManager.Instance.MonsterBulletExpPool.Get();
                explosionObj.GetComponent<MonsterBulletFatExplosion>().ActiveTime = Time.time;
                explosionObj.transform.position = hit.point;
                PlayerControl playerControl = hit.transform.GetComponent<PlayerControl>();
                if (playerControl)// 如果碰到角色 对角色造成伤害
                {
                    playerControl.Hit(Damage);
                }
                Stop();
            }
            
        }

    }

    public void Stop(bool playExp = false)
    {
        if (playExp)
        {
            GameObject explosionObj = GameManager.Instance.MonsterBulletExpPool.Get();
            explosionObj.GetComponent<MonsterBulletFatExplosion>().ActiveTime = Time.time;
            explosionObj.transform.position = transform.position;
        }
        GameManager.Instance.MonsterBulletPool.Release(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, DetectionR);
    }
}
