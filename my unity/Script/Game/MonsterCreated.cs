using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterCreated : MonoBehaviour
{
    public float GenerateR; // 生成半径
    public GameObject MonsterPrefab; // 生成的敌人
    public float WaitTime; // 等待多久开始生成
    public float GenerateInterval; // 生成间隔时间

    private float ActiveTime; // 该生成器激活时间
    private float LastGenerateTime; // 上一次生成时间
    public bool GenerateNow; // 是否立即生成敌人

    // Start is called before the first frame update
    void Start()
    {
        LastGenerateTime = Time.time;
        ActiveTime = Time.time;
        if (GenerateNow)
        {
            Generate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - ActiveTime >= WaitTime)
        {
            if (Time.time - LastGenerateTime >= GenerateInterval)
            {
                Generate();
            }
        }
    }

    // 生成敌人
    private void Generate()
    {
        LastGenerateTime = Time.time;
        Vector2 ranomdpos = Random.insideUnitCircle;
                
        Vector3 pos = transform.position + new Vector3(ranomdpos.x, 0, ranomdpos.y) * GenerateR;
        GameObject monster = Instantiate(MonsterPrefab);
        monster.transform.position = pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, GenerateR);
    }
}
