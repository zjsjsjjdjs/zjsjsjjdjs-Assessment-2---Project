using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public CapsuleCollider capcol;
    private Vector3 point1;
    private Vector3 point2;
    private float radius;

    private void Awake()
    {
        radius = capcol.radius;
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        point1 = transform.position + transform.up * (radius - 0.7f);
        point2 = transform.position + transform.up * capcol.height - transform.up * (radius - 0.7f);

        Collider[] outputCols = Physics.OverlapCapsule(point1, point2, radius, LayerMask.GetMask("Ground"));

        if (outputCols.Length!=0)
        {
            SendMessageUpwards("IsGround");
        }
        else
        {
            SendMessageUpwards("IsNotGround");
        }
    }
}
