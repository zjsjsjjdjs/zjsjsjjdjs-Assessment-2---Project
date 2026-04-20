using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boob : MonoBehaviour
{
    public GameObject 失败;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            //
            if (score.得分 < 9)
            {
                score.得分++;
                Destroy(gameObject);
            }
            else
            {
                失败.SetActive(true);
            }
           
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
