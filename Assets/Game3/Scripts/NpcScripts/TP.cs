using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TP : MonoBehaviour
{

    public GameObject target;

    public GameObject Tips;

    private float distance;
    private void Update()
    {
        distance = (gameObject.transform.position - target.transform.position).magnitude;
        if (distance <= 2f)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {

                Tips.SetActive(false);
                SceneManager.LoadScene(1);

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
            Cursor.lockState = CursorLockMode.None;

        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Tips.SetActive(false);

            //  Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
