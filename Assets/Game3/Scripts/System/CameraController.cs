using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerx;
    private GameObject model;
    private GameObject camera;

    //UI
    public GameObject bag;
    public GameObject task;
    public GameObject end;
    public GameObject talk;


    private Vector3 cameraDampVelocity;
    [SerializeField]
    private float cameraDampValue = 0.1f;  //越大镜头延迟越长
    [SerializeField]
    private LockTarget lockTarget;
 

    public PlayerInput pi;
    public float horizontalSpeed;
    public float verticalSpeed;
    public Image lockDot;
    public bool lockState;
    
    private void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        model = playerHandle.GetComponent<ActorController>().model;

        Cursor.lockState = CursorLockMode.Locked;  // 隐藏鼠标按钮

        camera = Camera.main.gameObject;
        lockDot.enabled = false;

        lockState = false;
    }
    private void Update()
    {
        if (lockTarget!=null)
        {
            lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0,lockTarget.halfHeight,0) );
            if (Vector3.Distance(model.transform.position,lockTarget.obj.transform.position)>10.0f)
            {
                lockTarget = null;
                lockDot.enabled = false;
                lockState = false;
            }
        }



        //按CapLock 锁定鼠标   按V解锁鼠标
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
           
                Cursor.lockState = CursorLockMode.Locked;           
           
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Cursor.lockState = CursorLockMode.None;
        }


    }

   
    void FixedUpdate()
    {
        if (lockTarget==null)  //锁中物体优化Player模型
        {
            Vector3 tempModelEuler = model.transform.eulerAngles;
            
                playerHandle.transform.Rotate(Vector3.up, pi.Viewright * horizontalSpeed* Time.fixedDeltaTime );       
                //cameraHandle.transform.Rotate(Vector3.right, pi.Viewup * -verticalSpeed* Time.deltaTime);
                //需要限定可以旋转的角度范围（-35~60） 会有同位角的问题

                tempEulerx -= pi.Viewup * verticalSpeed * Time.fixedDeltaTime;
                tempEulerx = Mathf.Clamp(tempEulerx, -35, 60);
                cameraHandle.transform.localEulerAngles = new Vector3(tempEulerx, 0, 0);
            


            model.transform.eulerAngles = tempModelEuler;
        }
        else
        {
            Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
            cameraHandle.transform.LookAt(lockTarget.obj.transform);
        }

        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampVelocity, cameraDampValue);
        camera.transform.LookAt(cameraHandle.transform);
    }






    public void LockUnLock()  //使用OverlapBox方法 去检测前方是否有物体  
    {
        
            Vector3 modelOrigin1 = model.transform.position;
            Vector3 modelOrigin2 = modelOrigin1+new Vector3(0,1,0);
            Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;

            Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(5f, 5f, 5f),model.transform.rotation,LayerMask.GetMask("Enemy"));

            if (cols.Length==0)//当索敌为空时 可以快速再次索敌
            {
                lockTarget = null;
                lockDot.enabled = false;
                lockState=false;

            }
            else
            {
                foreach (var col in cols)
                {
                   if (lockTarget!=null && lockTarget.obj==col.gameObject)//必须判断是否为空
                   {
                    lockTarget = null;
                    lockDot.enabled = false;
                    lockState = false;
                    break;   //解决无法二次解锁的问题
                    }

                    lockTarget = new LockTarget( col.gameObject,col.bounds.extents.y );
                    lockDot.enabled = true;
                    lockState = true;
                    break;
                }
            }
       
    }
    private class LockTarget
    {
        public GameObject obj;
        public float halfHeight;
        public LockTarget(GameObject _obj,float _halfHeight)
        {
            obj = _obj;
            halfHeight = _halfHeight;
        }
    }
}
