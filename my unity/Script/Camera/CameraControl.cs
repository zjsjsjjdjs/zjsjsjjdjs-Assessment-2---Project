using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 相机控制 负责相机的位置更新 相机的旋转 相机的抖动
/// </summary>
public class CameraControl : MonoBehaviour
{
    public Transform TargetTr; // 相机跟随的目标
    public Transform LookAtTr; // 相机看向的目标
    public Vector3 Deviation; // 相机与目标点的偏差

    private Vector3 NowDeviation; // 当前偏差
    private Vector3 LookAtOffset; // 与看向点的偏差
    
    public float RotateSpeed; // 旋转速度
    public float JitterIntensity = 0.1f; // 抖动强度
    
    private Vector3 shakePos = Vector3.zero; // 设置坐标的偏移量 用于设置抖动的偏差
    private bool OnFire; // 是否在开火 用于判断是否需要相机抖动
    private PlayerControl _playerControl;
    
    // Start is called before the first frame update
    void Start()
    {
        NowDeviation = Deviation;
        LookAtOffset = LookAtTr.position - TargetTr.position;
        _playerControl = FindObjectOfType<PlayerControl>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // 旋转相机
        float x = Input.GetAxis("Mouse X");
        x *= Time.deltaTime * RotateSpeed;
        NowDeviation = Quaternion.Euler(0, x, 0) * NowDeviation;
        LookAtOffset = Quaternion.Euler(0, x, 0) * LookAtOffset;
        
        shakePos = Vector3.zero;
        if(OnFire && !_playerControl.isRoll && _playerControl.nowHP >0) // 相机抖动 条件是在开火 角色没有在翻滚 角色的Hp大于0
        {
            shakePos = Random.insideUnitSphere * JitterIntensity;
        }
        transform.position = TargetTr.position + NowDeviation;
        transform.LookAt(TargetTr.position + LookAtOffset + shakePos);
        // transform.forward = VisualAngleQutY * NowDeviation;
        
    }

    public void OnFireByPlayer(bool fire = false)
    {
        OnFire = fire;
    }
}
