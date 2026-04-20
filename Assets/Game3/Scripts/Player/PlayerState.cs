using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public Text ExpText;
    private bool isGameOver=false;
    public int BloodLevel = 10;
    public int maxBlood;
    public int bloodUpdate;
    public BloodBar bloodBar;
    

    private PlayerInput pi;
    private Animator animator;


    public int GameApple { get; set; }
    public int GameGlod { get; set; }
    public int GameExp = 1;

    private void Start()
    {
        maxBlood = setMaxBlood();
        bloodUpdate = maxBlood;
        bloodBar.SetMaxBlood(maxBlood);
        animator = GetComponentInChildren<Animator>();
        pi = GetComponent<PlayerInput>();

    }

    private int setMaxBlood()
    {
        maxBlood = BloodLevel * 10;
        return maxBlood;
    }

    public void TackDamage(int damage)
    {
        
        bloodUpdate = bloodUpdate - damage;
        bloodBar.SetUpdate(bloodUpdate);
        animator.SetTrigger("damage");
        if (bloodUpdate<=0)
        {
            bloodUpdate = 0;
            pi.inputEnabled = false;
            animator.SetBool("isDead", true);
            EventCenter.Broadcast(EventRole.GAMEOVER);


            if (isGameOver) return;
            GameFacade.Instance.PlaySound("死亡倒地");
            
            isGameOver =true;
            
        }
    }

    
    public void Recover(int recover)
    {
        bloodUpdate =bloodUpdate+recover;
        bloodBar.SetUpdate(bloodUpdate);  
    }





    public void UpGradeApple(int num)
    {
        GameApple += num;
    }

    public void UpGradeGlod(int num)
    {
        GameGlod += num;
    }
    
    public void UpGradeExp(int num)
    {
        
        GameExp += num;
        ExpText.text = GameExp.ToString();
    }


}
