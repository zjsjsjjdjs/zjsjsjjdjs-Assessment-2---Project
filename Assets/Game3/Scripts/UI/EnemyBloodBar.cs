using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBloodBar : MonoBehaviour
{
    private EnemyState enemyState;   
    public GameObject BloodBar;
    public Image bloodSlider;
    public Transform barPos;
    public bool canSee;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyState = GetComponent<EnemyState>();
        bloodSlider.fillAmount = 1;
        
    }
    private void Update()
    {
        
        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 target = new Vector3(cameraPos.x, BloodBar.transform.position.y, cameraPos.z);
        BloodBar.transform.LookAt(target);
        BloodBar.transform.position = barPos.position;


        bloodSlider.fillAmount = (float)enemyState.bloodUpdate / enemyState.maxBlood;


        //一定距离血条显示
        BloodBar.SetActive(canSee);
        float distance = (gameObject.transform.position - player.transform.position).magnitude;
        if (distance <= 7f)
        {
            canSee = true;
        }
        else
        {
            canSee = false;
        }
    }
}
