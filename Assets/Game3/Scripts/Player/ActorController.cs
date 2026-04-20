using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject weapon;
    public GameObject model;
    public GameObject Bag;
    public GameObject Task;
    public GameObject EndPanel;
    public GameObject TalkNPC;
    public GameObject SelfInformation;

    public CameraController camcon;
    public float walkSpeed = 3.0f;
    public float runSpeed;
    public float jumpHeight;
    public float rollVelocity;
    public float jabVelocity;


    [Header("===== Friction Setting=====")]
    [SerializeField]//把private显示出来
    private PhysicMaterial frictionOne;
    [SerializeField]//把private显示出来
    private PhysicMaterial frictionZero;

    private PlayerInput pi;
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;
    private bool canAttack;

    private CapsuleCollider col;
    private float lerpTarget;              //  解决动画层权重转变生硬
    private Vector3 deltaPos;

    [SerializeField]
    [Header("平台移动 锁死是保持原速度")]
    private bool lockPlannar = false;
    [SerializeField]
    private bool trackDirection = false;  //解决翻滚 跳跃朝向目标的问题




    void Awake() //GetCompoent尽量在Awake里面
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();


    }

    //*暂存变量便于思考*

    void Update()
    {
        if (pi.lockon)
        {
            camcon.LockUnLock();
        }



        if (camcon.lockState == false)
        {
            float targetRunMulti = ((pi.run) ? 2.0f : 1.0f);
            anim.SetFloat("forward", (pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), targetRunMulti, 0.7f)));
            anim.SetFloat("right", 0);

        }
        else
        {
            Vector3 localDvecz = transform.InverseTransformDirection(pi.Dvec);
            anim.SetFloat("forward", localDvecz.z * ((pi.run) ? 2.0f : 1.0f));
            anim.SetFloat("right", localDvecz.x * ((pi.run) ? 2.0f : 1.0f));
        }






        if (pi.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
            GameFacade.Instance.PlaySound("跳跃");
        }
        if (rigid.velocity.magnitude > 1.0f)
        {
            anim.SetTrigger("roll");
        }
        if (pi.attack && CheckState("ground") && canAttack)
        {
            anim.SetTrigger("attack");
        }
        if (pi.denfense)
        {
            anim.SetBool("denfense", true);

        }
        else
        {
            anim.SetBool("denfense", false);
        }




        if (pi.bag)
        {
            Bag.SetActive(true);
            GameFacade.Instance.PlaySound("UI点击");
        }
        if (pi.task)
        {
            Task.SetActive(true);
            GameFacade.Instance.PlaySound("UI点击");
        }
        if (pi.endPanel)
        {
            EndPanel.SetActive(true);
            GameFacade.Instance.PlaySound("UI点击");
        }
        if (pi.information)
        {
            SelfInformation.SetActive(true);
            GameFacade.Instance.PlaySound("UI点击");
        }





        if (Bag.activeSelf || TalkNPC.activeSelf || EndPanel.activeSelf || Task.activeSelf )//不能攻击 以及鼠标解锁
        {
            canAttack = false;
            Cursor.lockState = CursorLockMode.None;

        }
        else
        {
            canAttack = true;
        }




        //解决行走到奔跑动作的生硬转变 缓动效果
        //Lerp函数 float
        //Slerp函数 Vector3
        if (camcon.lockState == false)  //如果未索敌 模型运动和之前一样
        {
            if (pi.Dmag > 0.1f)
            {
                model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.2f);//解决转向生硬   缓动效果
            }
            if (lockPlannar == false)
            {
                planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runSpeed : 1.0f);//移动速度
            }
        }
        else                         //如果索敌 实现模型运动朝向目标
        {
            if (trackDirection == false)
            {
                model.transform.forward = transform.forward;
            }
            else
            {
                model.transform.forward = planarVec.normalized;
            }
            if (lockPlannar == false)
            {
                planarVec = pi.Dvec * walkSpeed * ((pi.run) ? runSpeed : 1.0f);
            }
        }

    }
    void FixedUpdate()//   固定时间0.02s
    {
        rigid.position += deltaPos;
        //rigid.position += planarVec * Time.fixedDeltaTime;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;



    }



    private bool CheckState(string stateName, string layerName = "Base Layer")//判断动画现在是否在BaseLayer层的动画
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
        return result;
    }











    /// <summary>
    //Message block 信息块  优化动作/ 
    /// </summary>

    public void OnJump()
    {
        pi.inputEnabled = false;     //解决跳起来 人物可以左右转向的问题
        lockPlannar = true;
        thrustVec = new Vector3(0, jumpHeight, 0);//高度
        trackDirection = true;
    }


    public void IsGround()
    {
        anim.SetBool("isGround", true);
    }
    public void IsNotGround()
    {
        anim.SetBool("isGround", false);
    }



    public void OnGroundEnter()//解决ExitJump空中能移动的问题  ExitJump移除
    {
        pi.inputEnabled = true;
        lockPlannar = false;
        canAttack = true;
        col.material = frictionOne;
        trackDirection = false;
    }
    public void OnGroundExit()
    {
        col.material = frictionZero;
    }





    public void OnRollEnter()
    {
        pi.inputEnabled = false;     //解决人物可以左右转向的问题
        lockPlannar = true;
        thrustVec = new Vector3(0, rollVelocity, 0);
        trackDirection = true;
    }


    public void OnJabEnter()
    {
        pi.inputEnabled = false;
        lockPlannar = true;

    }
    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity") * jabVelocity;
    }

    public void OnAttackIdleEnter()
    {
        pi.inputEnabled = true;
        lerpTarget = 0f;
    }
    public void OnAttackIdleUpdate()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("attack")), lerpTarget, 0.05f));
    }
    public void OnAttack1hAEnter()
    {
        GameFacade.Instance.PlaySound("挥刀");
        pi.inputEnabled = false;
        weapon.GetComponent<BoxCollider>().enabled = true;
        lerpTarget = 1.0f;

    }

    public void OnAttack1hAExit()
    {

    }
    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");//Animation 里面curves  随着动画播放改变数值

        // 解决动画层权重转变生硬
        float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);
    }

    public void OnAttack1hBEnter()
    {
        GameFacade.Instance.PlaySound("挥刀");
        pi.inputEnabled = false;
        weapon.GetComponent<BoxCollider>().enabled = true;
    }
    public void OnAttack1hBUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hBVelocity");//Animation 里面curves  随着动画播放改变数值
        //同时解决模型在攻击时可以跳跃的问题
    }

    public void OnAttack1hCEnter()
    {
        GameFacade.Instance.PlaySound("挥刀");
        pi.inputEnabled = false;
        weapon.GetComponent<BoxCollider>().enabled = true;
    }
    public void OnAttack1hCUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hCVelocity");//Animation 里面curves  随着动画播放改变数值
                                                                                 //同时解决模型在攻击时可以跳跃的问题
    }





    public void OnUpdateRootMotion(object _deltaPos)
    {
        if (CheckState("attack1hA", "attack") || CheckState("attack1hB", "attack") || CheckState("attack1hC", "attack"))//攻击时才调用
        {
            deltaPos += (Vector3)_deltaPos;

        }
    }  //使得动画在播放时 可以产生位移增加沉浸感














}
