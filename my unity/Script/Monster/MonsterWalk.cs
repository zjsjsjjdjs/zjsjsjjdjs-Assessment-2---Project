using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWalk : StateMachineBehaviour
{
    private MonsterControl _monsterControl;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_monsterControl == null)
        {
            _monsterControl = animator.GetComponent<MonsterControl>();
        }    
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 判断角色是否在攻击范围内 如果在则跳转到攻击动画并停止移动
        if (Vector3.Distance(_monsterControl._playerControl.transform.position, animator.transform.position) <= _monsterControl.AttackDis)
        {
            animator.SetBool("Attack", true);
            animator.SetBool("Walk", false);
            _monsterControl._NavMeshAgent.isStopped = true;
        }
        else // 判断角色是否在攻击范围内 如果不在则移动并设置寻路目标
        {
            _monsterControl._NavMeshAgent.isStopped = false;
            _monsterControl._NavMeshAgent.SetDestination(_monsterControl._playerControl.transform.position);
            animator.SetBool("Walk", true);
            animator.SetBool("Attack", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
