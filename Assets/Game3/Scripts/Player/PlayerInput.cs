using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("=====移动键位设置=====")]
    public string keyUp;
    public string keyDown;
    public string keyLeft;
    public string keyRight;


    [Header("冲刺 跳跃 锁定目标 背包 任务")]
    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;
    public string keyE;



    [Header("=====技能键位=====")]
    public string Skill1;
    public string Skill2;
    public string Skill3;
    public string Skill4;

    

    [Header("=====视角键位及设置=====")]

    public string keyViewUp;
    public string keyViewDown;
    public string keyViewLeft;
    public string keyViewRight;


    public string ViewPointKey;
    public bool LockMouse;
    public float ViewXSpeed=1f;
    public float ViewYSpeed=1f;
   


    [Header("=====输出信号=====")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;

    public float Viewup;
    public float Viewright;


    public bool run;  
    public bool jump;
    public bool attack;
    public bool denfense;
    public bool lockon;

    public bool bag;
    public bool task;
    public bool endPanel;
    public bool information;

    public bool skill1;
    public bool skill2;
    public bool skill3;
    public bool skill4;
    //private bool lastJump;
    //1.pressing signals
    //2.trigger signals
    //3. double trigger signals


    [Header("=====其他=====")]
    public bool inputEnabled = true; //Flag

    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;



    void Update()
    {
        //将WSAD转为数字信号  -1~1  并且平滑的变动数值 
    
        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        if (inputEnabled==false)
        {
            targetDup = 0;
            targetDright = 0;
        }

        //平滑的变动数值
        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.5f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.5f);
        
        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));

        float Dup2 = tempDAxis.y;
        float Dright2 = tempDAxis.x;

        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));//向量大小
        Dvec= Dright2 * transform.right + Dup2 * transform.forward;//向量





        //键位设置

        run = Input.GetKey(keyA);
        jump = Input.GetKeyDown(keyB);
        lockon = Input.GetKeyDown(keyC);

        bag = Input.GetKeyDown(keyD);
        task=Input.GetKeyDown(keyE);
        endPanel = Input.GetKeyDown(KeyCode.Escape);
        information = Input.GetKeyDown(KeyCode.G);


        attack = Input.GetMouseButtonDown(0);
        denfense = Input.GetMouseButton(1);

        
        skill1 = Input.GetKeyDown(Skill1);
        skill2 = Input.GetKeyDown(Skill2);
        skill3 = Input.GetKeyDown(Skill3);
        skill4 = Input.GetKeyDown(Skill4);


     


        //视角设置

        if (LockMouse = Input.GetKey(ViewPointKey))
        {
            Viewup = ((Input.GetKey(keyViewUp) ? 1.0f : 0) - (Input.GetKey(keyViewDown) ? 1.0f : 0)) * ViewYSpeed;
            Viewright = ((Input.GetKey(keyViewRight) ? 1.0f : 0) - (Input.GetKey(keyViewLeft) ? 1.0f : 0)) * ViewXSpeed;
        }
        else
        {
            Viewright = Input.GetAxis("Mouse X") * ViewXSpeed;
            Viewup = Input.GetAxis("Mouse Y") * ViewYSpeed;
        }


    }
    private Vector2 SquareToCircle(Vector2 input)//椭圆映射法 解决斜角移动加速
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;
    }


}
