using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private bool isGameOver = false;
    private PlayerInput pi;
    private GameObject player;
    public Animator anim;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pi =player.GetComponent<PlayerInput>();
       
    }
    private void Start()
    {     
        SpawnEnemy();
        SpawnApple();
        SpawnHP();
        EventCenter.AddListener(EventRole.GAMEOVER, StopSpawn);
    }


    private void OnDisable()
    {
        EventCenter.RemoveListener(EventRole.GAMEOVER, StopSpawn);
    }
    private void Update()
    {
        if (isGameOver) return;
   
    }

    private void StopSpawn()
    {
        //取消该脚本
        CancelInvoke();
        isGameOver = true;
    }

    private void SpawnEnemy()
    {
        GameFacade.Instance.LoadEnemy("EnemyHandle1");
        GameFacade.Instance.LoadEnemy("EnemyHandle2");
        GameFacade.Instance.LoadEnemy("EnemyHandle3");
    }

   
    private void SpawnApple()
    {
        for (int i = 0; i < 5; i++)
        {
        float xRos =UnityEngine.Random.Range(-12.5f, -8.0f);
        float zRos =UnityEngine.Random.Range(3.5f, 8.5f);
        GameObject apple = GameFacade.Instance.LoadApple("apple");
        apple.transform.position = new Vector3(xRos, transform.position.y, zRos);          
        }
    }
    private void SpawnHP()
    {
        GameObject HP = GameFacade.Instance.LoadApple("HP");
    }

}
