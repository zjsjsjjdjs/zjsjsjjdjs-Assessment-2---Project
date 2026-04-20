using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public class EnemyState : MonoBehaviour
{
    
    public int BloodLevel = 10;
    public int maxBlood;
    public int bloodUpdate;

    private Animator animator;
    private NavMeshAgent agent;
    private Transform target;
    private float distance;
    private bool isDead=false;
    private bool isGameOver = false;

    public GameObject startPos;

    public int GameEnemy;

    private void Start()
    {

        bloodUpdate = maxBlood;
        animator = GetComponentInChildren<Animator>();

        //敌人的追踪
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        EventCenter.AddListener(EventRole.GAMEOVER, StopMove);
    }

    private void StopMove()
    {
        isGameOver = true;
    }

    private void OnDisable()
    {
      EventCenter.RemoveListener(EventRole.GAMEOVER, StopMove);
    }

    private void Update()
    {
        if (isGameOver) return;//游戏结束 返回

        distance = (gameObject.transform.position - target.position).magnitude;
        if (!animator.GetBool("isDead"))
        {
            if (distance<=5f)
            {
                animator.SetBool("isWalk", true);
                agent.SetDestination(target.position);
                if (distance<2f)
                {
                    animator.SetBool("isWalk", false);
                    animator.SetTrigger("attack");
                }
            }
            else
            {
              animator.SetBool("isWalk", true);
              agent.stoppingDistance=2f;
              agent.SetDestination(startPos.transform.position);
                 if (agent.remainingDistance < agent.stoppingDistance)
                    {
                        animator.SetBool("isWalk", false);
                    }
            }

        }
        else
        {
            
        }


        if (isDead)
        {
            GameObject glod = GameFacade.Instance.LoadGlod("glod");
            glod.transform.position = gameObject.transform.position + transform.up;
            UpGradeEnemy(1);
            isDead = false;
        }

    }

    

    
    public void TackDamage(int damage)
    {

        bloodUpdate = bloodUpdate - damage;
        GameFacade.Instance.PlaySound("击打");
        if (bloodUpdate <= 0)
        {
            bloodUpdate = 0;
            animator.SetBool("isDead", true);
            isDead = true;
            Destroy(gameObject, 3f);
           
        }

        animator.SetTrigger("damage");
    }

    public void UpGradeEnemy(int num)
    {
        GameEnemy += num;
        if (GameEnemy>=1)
        {
            GameEnemy = 1;
        }
    }


}
