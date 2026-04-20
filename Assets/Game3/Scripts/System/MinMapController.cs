using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMapController : MonoBehaviour
{
    private Transform Playertransform;
    private void Start()
    {
        Playertransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        gameObject.transform.position = new Vector3(Playertransform.position.x, 10f, Playertransform.position.z);
    }
}
