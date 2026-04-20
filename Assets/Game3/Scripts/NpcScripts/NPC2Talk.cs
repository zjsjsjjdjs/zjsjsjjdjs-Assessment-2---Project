using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC2Talk : MonoBehaviour
{
    public GameObject talkbox;
    public Text talk;
    public Text npcName;
    public GameObject npcimage1;
    public GameObject npcimage2;
    public GameObject npcimage3;
    public GameObject ApplyButton2;
    public GameObject ApplyButton1;
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
        

            if (Input.GetKeyDown(KeyCode.F) || talkbox.activeSelf)
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

            Tips.SetActive(true);
            ApplyButton1.SetActive(false);
            ApplyButton2.SetActive(true);
            ApplyButton3.SetActive(false);
            talk.text = "我可以带你去地点C，但是前方有守卫，先解决守卫吧";
            npcName.text = "首领";
            Cursor.lockState = CursorLockMode.None;
            npcimage2.SetActive(true);
            npcimage1.SetActive(false);          
            npcimage3.SetActive(false);
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
