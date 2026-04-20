using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_4 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void 退出游戏()
    { 
    
        Application.Quit(); 
    }
    public void 重新开始()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
