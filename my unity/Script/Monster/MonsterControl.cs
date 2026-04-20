using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterControl : MonoBehaviour
{
    public float AttackDis; // 攻击距离
    [HideInInspector]
    public PlayerControl _playerControl;
    [HideInInspector]
    public NavMeshAgent _NavMeshAgent; // 寻路组件
    public Transform ShootTr; // 攻击发射的位置

    public ParticleSystem HPBar; // 血条
    public float MaxHP; // 最大生命值
    private float nowHP; // 当前生命值
    private float startHPbarX; // 开始时血条的x轴缩放 (因为这里是用的粒子特效制作的血条比较特殊)

    private bool bLookAtPlayer; // 是否看向角色
    ParticleSystem.MainModule hpBarMain; // 血量粒子特效的MainModule
    private ParticleSystem.MinMaxCurve minMaxCurve; // 血条特效的MinMaxCurve 用于更改x轴缩放
    private bool bLife; // 是否存活
    private void Awake()
    {
        bLife = true;
        _playerControl = FindObjectOfType<PlayerControl>();
        _NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        nowHP = MaxHP;
        hpBarMain = HPBar.main;
        startHPbarX = hpBarMain.startSizeX.constant;
        minMaxCurve = hpBarMain.startSizeX;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bLife)
        {
            return;
        }
        
        if (bLookAtPlayer)
        {
            transform.LookAt(_playerControl.transform.position);
        }
    }

    public void OnAttackStart()
    {
        bLookAtPlayer = true;
    }

    public void OnAttackEnd()
    {
        bLookAtPlayer = false;
    }
    
    // 攻击 有动画事件调用
    public void OnAttack()
    {
        GameObject bulletObj = GameManager.Instance.MonsterBulletPool.Get();
        bulletObj.transform.position = ShootTr.position;
        bulletObj.transform.rotation = ShootTr.rotation;
        bulletObj.GetComponent<MonsterBullet>().ActiveTime = Time.time;
    }

    public void OnDie()
    {
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, AttackDis);
    }

    // 受击方法
    public void Hit(float Damge)
    {
        if (nowHP <= 0)
        {
            return;
        }
        
        // 更新血量
        nowHP -= Damge;
        if (nowHP <= 0)
        {
            nowHP = 0;
            GetComponent<Animator>().SetBool("Die", true);
            HPBar.Stop();
            bLife = false;
            GameManager.Instance.AddCount();
        }

        // 刷新血条
        minMaxCurve.constant = nowHP / MaxHP * startHPbarX;
        hpBarMain.startSizeX = minMaxCurve;
        HPBar.Stop();
        HPBar.Clear();
        HPBar.Play();
    }
}
