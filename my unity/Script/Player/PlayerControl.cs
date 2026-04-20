using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 角色管理
/// </summary>
public class PlayerControl : MonoBehaviour
{
    private Animator _animator; // 角色动画控制器
    [Range(0,1)]
    public float SpeedCoefficient; // 移动的平滑过渡

    public float IdleTime; // 间隔多久没有攻击 回到闲置状态
    private float LastAttackTimeByIdle; // 上一次攻击时间 用于回到闲置状态判断

    public float AttackIntervalTime; // 攻击间隔时间
    private float LastAttackTime; // 上一次攻击时间

    private bool bAiming; // 是否在攻击状态 (攻击状态的移动 与 闲置状态的移动不一样)
    private Transform cameraTr; // 相机的Transform

    public float WalkSpeed; //  速度设置 因为使用的动画的根移动 所以该参数没有使用
    public float AttackSpeed; //

    [Range(0,1)]
    public float SteeringCoefficient; // 旋转的平滑系数

    [HideInInspector]
    public bool isRoll; // 是否在翻滚
    public float RollSpeed; // 翻滚的速度
    public ParticleSystem ShootFX; // 开枪枪口粒子
    public GameObject BulletPrefab; // 子弹预制体
    public GameObject BulletExplosionPrefab; // 子弹预制体
    public Transform ShootTr; // 枪口
    private List<GameObject> BulletList; // 子弹对象池
    private List<GameObject> BulletExplosionList; // 子弹对象池

    public float MaxHP; // 最大血量
    [HideInInspector]
    public float nowHP;  // 当前血量
    public Slider HPBar; // 血条 使用滑动条
    
     public AudioSource _fire;// 开枪声音
    
    private CameraControl _cameraControl;
    // Start is called before the first frame update
    void Start()
    {
        nowHP = MaxHP;
        HPBar.value = 1;
        
        _animator = GetComponent<Animator>();
        cameraTr = Camera.main.transform;
        LastAttackTime = -IdleTime;
        _cameraControl = FindObjectOfType<CameraControl>();

        BulletList = new List<GameObject>();
        BulletExplosionList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nowHP <= 0 || Time.timeScale <= 0)
        {
            return;
        }
        
        Attack();
        StateUpdate();
        Move();
    }

    // 移动
    private void Move()
    {
        // 根据键盘输入移动
        Vector3 MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _animator.SetFloat("X", MoveDir.x);
        _animator.SetFloat("Y", MoveDir.z);
        float speed = 0;
        float lastspeed = _animator.GetFloat("Speed");
        if (MoveDir.magnitude > 0.01f)
        {
            if (MoveDir.magnitude > 1)
            {
                MoveDir.Normalize();
            }

            speed = MoveDir.magnitude;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = Mathf.Lerp( lastspeed,MoveDir.magnitude * 2, SpeedCoefficient);
            }
        }
        
        _animator.SetFloat("Speed", Mathf.Lerp( lastspeed,speed, SpeedCoefficient));
        

        Vector3 cameraForward = cameraTr.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        if (!isRoll && Input.GetKeyDown(KeyCode.Space)) // 翻滚触发
        {
            MoveDir = Quaternion.FromToRotation(Vector3.forward, cameraForward) * MoveDir;
            transform.LookAt(transform.position + MoveDir);
            _animator.SetTrigger("Roll");
            isRoll = true;
        }
        
        if(!isRoll) // 如果没有翻滚调整角色方向
        {
            Quaternion nowQut = transform.rotation;
            if (bAiming) // 如果是攻击状态则转向为相机正前方
            {
                transform.LookAt(transform.position + cameraForward);
                // transform.Translate(MoveDir * AttackSpeed * Time.deltaTime);
            }
            else // 如果是闲置状态 朝向为移动方向
            {
                MoveDir = Quaternion.FromToRotation(Vector3.forward, cameraForward) * MoveDir;
                transform.LookAt(transform.position + MoveDir);
                // transform.position += MoveDir  * speed * WalkSpeed * Time.deltaTime;
            }
            transform.rotation = Quaternion.Lerp(nowQut, transform.rotation, SteeringCoefficient); // 平滑旋转
        }
        else
        {
            transform.Translate(Vector3.forward * RollSpeed * Time.deltaTime); // 翻滚移动
        }
    }

    // 攻击
    private void Attack()
    {
        if (isRoll)
        {
            return;
        }
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
            LastAttackTimeByIdle = Time.time;
            if (Time.time - LastAttackTime >= AttackIntervalTime)
            {
                LastAttackTime = Time.time;
                _animator.SetTrigger("Shoot"); // 播放攻击动画
                _cameraControl.OnFireByPlayer(true); // 相机抖动
                ShootFX.Play(); // 枪口火花特效播放
                _fire.Play(); // 播放开枪声音

                // 获取并设置子弹方向,朝向
                GameObject BulletObj = GetBulletObject();
                
                Vector3 cameraForward = cameraTr.forward;
                cameraForward.y = 0;
                cameraForward.Normalize();

                Vector3 trForward = transform.forward;
                trForward.y = 0;
                trForward.Normalize();

                Vector3 bulletPos = Quaternion.FromToRotation(trForward, cameraForward) * (ShootTr.position - transform.position);
                Vector3 bulletDir = Quaternion.FromToRotation(trForward, cameraForward) * ShootTr.forward;

                BulletObj.transform.position = bulletPos + transform.position ;
                // BulletObj.transform.rotation = ShootTr.rotation;
                BulletObj.transform.rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(bulletDir.x, 0, bulletDir.z));
                BulletObj.SetActive(true);
                // BulletObj.GetComponent<ParticleSystem>().Play();
            }
        }
        else
        {
            _cameraControl.OnFireByPlayer();
        }
    }

    // 设置是攻击状态还是闲置状态
    private void StateUpdate()
    {
        if (Time.time - LastAttackTimeByIdle >= IdleTime)
        {
            bAiming = false;
        }
        else
        {
            bAiming = true;
        }
        
        _animator.SetBool("Aiming", bAiming);
    }

    // 从对象池中获取子弹
    public GameObject GetBulletObject()
    {
        GameObject bulletobj = null;
        int index = BulletList.FindIndex((obj) => !obj.activeSelf);
        if (index >= 0)
        {
            bulletobj = BulletList[index].gameObject;
            BulletList.RemoveAt(index);
        }
        else
        {
            bulletobj = Instantiate(BulletPrefab);
            bulletobj.GetComponent<Bullet_2>().PlayerControl = this;
        }
        bulletobj.GetComponent<Bullet_2>().ActiveTime = Time.time;
        return bulletobj;
    }

    // 将不用的子弹放回到对象池中
    public void SetBulletObject(GameObject effectObj)
    {
        // effectObj.GetComponent<ParticleSystem>().Stop();
        effectObj.SetActive(false);
        BulletList.Add(effectObj);
    }
    
    // 从对象池中获取子弹碰撞特效
    public GameObject GetBulletExplosionPrefabObject()
    {
        GameObject bulletobj = null;

        int index = BulletExplosionList.FindIndex((obj) => !obj.activeSelf);
        if (index >= 0)
        {
            bulletobj = BulletExplosionList[index].gameObject;
            BulletExplosionList.RemoveAt(index);
        }
        else
        {
            bulletobj = Instantiate(BulletExplosionPrefab);
            bulletobj.GetComponent<BulletFatExplosion>().playerControl = this;
        }
        bulletobj.GetComponent<BulletFatExplosion>().ActiveTime = Time.time;
        return bulletobj;
    }

    // 将不用的子弹碰撞特效放回到对象池中
    public void SetBulletExplosionPrefabObject(GameObject effectObj)
    {
        // effectObj.GetComponent<ParticleSystem>().Stop();
        BulletExplosionList.Add(effectObj);
    }
    
    // 角色受击
    public void Hit(float Damge)
    {
        if (nowHP <= 0)
        {
            return;
        }
        
        nowHP -= Damge;
        if (nowHP <= 0)
        {
            nowHP = 0;
            OnDead();
        }
        
        HPBar.value = nowHP / MaxHP ;
    }
    
    // 角色死亡 播放死亡动画
    public void OnDead()
    {
        _animator.SetBool("Dead", true);
    }

  
    public void FootStep()
    {
        
    }
    
    // 动画事件 翻滚开始
    public void RollSound()
    {
        isRoll = true;
    }
    
    // 动画事件
    public void CantRotate()
    {
       
    }
    
    // 动画事件 翻滚结束
    public void EndRoll()
    {
        isRoll = false;
    }
}
