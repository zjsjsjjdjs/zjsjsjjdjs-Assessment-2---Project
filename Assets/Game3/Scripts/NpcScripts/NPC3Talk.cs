using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC3Talk : MonoBehaviour
{
    public GameObject talkbox;
    public Text talk;
    public Text npcName;
    public GameObject npcimage1;
    public GameObject npcimage2;
    public GameObject npcimage3;
    public GameObject ApplyButton1;
    public GameObject ApplyButton2;
    public GameObject ApplyButton3;
    public GameObject target;
    public GameObject Tips;
    public GameObject firstTask;

    private float distance;
    private void Update()
    {
        distance = (gameObject.transform.position - target.transform.position).magnitude;
        if (distance <= 2f)
        {
        if (Input.GetKeyDown(KeyCode.F)|| talkbox.activeSelf)
        {
            talkbox.SetActive(true);
            Tips.SetActive(false);
                firstTask.SetActive(false);
            }
        }
        else
        {
                   
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            
            Tips.SetActive (true);
                
                ApplyButton1.SetActive(false);
                ApplyButton2.SetActive(false);
                ApplyButton3.SetActive(true);

                talk.text = "去地点B找首领问问，有没有其他前往场景2的方法";
                npcName.text = "附近居民";
                Cursor.lockState = CursorLockMode.None;
                npcimage3.SetActive(true); 
                npcimage1.SetActive(false);
                npcimage2.SetActive(false);
               
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            talkbox.SetActive(false);            
            Tips.SetActive(false);
            
            //  Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
}
