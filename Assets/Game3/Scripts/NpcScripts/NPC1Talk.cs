using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPC1Talk : MonoBehaviour
{
    public GameObject talkbox;
    public Text talk;
    public Text npcName;

    public GameObject tip2;
    public GameObject enemy;

    public GameObject target;
    public GameObject Tips;

    private float distance;
    private void Update()
    {
        distance = (gameObject.transform.position - target.transform.position).magnitude;
        if (distance <= 2f)
        {

            Destroy(tip2);
            enemy.GetComponent<NpcShow>().enabled = false;
            if (Input.GetKeyDown(KeyCode.F) || talkbox.activeSelf)
            {
                talkbox.SetActive(true);
                Tips.SetActive(false);
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


            talk.text = "这是一场误会";
            npcName.text = "商人";
            Cursor.lockState = CursorLockMode.None;

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
