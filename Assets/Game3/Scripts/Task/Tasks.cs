using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tasks : MonoBehaviour
{
    public int id;
    private Text des;
    public Text need;
    public Text reward;
    public Button get;
    public TaskDataBase taskDataBase;

    public PlayerState playerState;
    public EnemyState enemyState;

    bool isFinish = false;
    bool reWard = true;
    private void Awake()
    {
        des = transform.Find("des").GetComponent<Text>();
        need = transform.Find("need").GetComponent<Text>();
        reward = transform.Find("reward").GetComponent<Text>();
        get = transform.Find("Get").GetComponent<Button>();
        playerState = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
        enemyState = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyState>();
    }
    
    private void Update()
    {
     

        if (isFinish==true)
        {
            gameObject.SetActive(false);
        }
        des.text = taskDataBase.Tasks[id].des;

        if (taskDataBase.Tasks[id].needGoods.goodsType == GoodsType.apple)
        {
            int currentApple = playerState.GameApple;//当前苹果数
            need.text = "条件:"+ currentApple.ToString() + "/" + taskDataBase.Tasks[id].needGoods.goodsCount.ToString();//任务进度
            if (currentApple>= taskDataBase.Tasks[id].needGoods.goodsCount)
            {
                get.gameObject.SetActive(true);//完成任务 按键显示
                get.onClick.AddListener(() =>
                {
                    if (reWard)
                    {
                    playerState.UpGradeExp(taskDataBase.Tasks[id].rewardGoods.goodsCount);//获得奖励
                    reWard = false;
                    }
                    GameFacade.Instance.PlaySound2("升级");
                    get.onClick.RemoveAllListeners();
                    isFinish = true;
                    get.transform.parent.gameObject.SetActive(false);//关闭任务
                });
            }
        }//苹果

        if (taskDataBase.Tasks[id].needGoods.goodsType == GoodsType.gold)
        {
            int currentGold = playerState.GameGlod;//当前金币数
            need.text = "条件:" + currentGold.ToString() + "/" + taskDataBase.Tasks[id].needGoods.goodsCount.ToString();//任务进度
            if (currentGold >= taskDataBase.Tasks[id].needGoods.goodsCount)
            {
                get.gameObject.SetActive(true);//完成任务 按键显示
                get.onClick.AddListener(() =>
                {
                    if (reWard)
                    {
                        playerState.UpGradeExp(taskDataBase.Tasks[id].rewardGoods.goodsCount);//获得奖励
                        reWard = false;
                    }
                    GameFacade.Instance.PlaySound2("升级");
                    isFinish = true;
                    get.onClick.RemoveAllListeners();
                    get.transform.parent.gameObject.SetActive(false);//关闭任务
                });
            }
        }//金币


        if (taskDataBase.Tasks[id].needGoods.goodsType == GoodsType.enemy)
        {
            
            int currentEnemy =enemyState.GameEnemy;//当前敌人数
            need.text = "条件:" + currentEnemy.ToString() + "/" + taskDataBase.Tasks[id].needGoods.goodsCount.ToString();//任务进度
            if (currentEnemy >= taskDataBase.Tasks[id].needGoods.goodsCount)
            {
                get.gameObject.SetActive(true);//完成任务 按键显示
                get.onClick.AddListener(() =>
                {
                    if (reWard)
                    {
                        playerState.UpGradeExp(taskDataBase.Tasks[id].rewardGoods.goodsCount);//获得奖励
                        reWard = false;
                    }
                    GameFacade.Instance.PlaySound2("升级");
                    isFinish = true;
                    get.onClick.RemoveAllListeners();
                    get.transform.parent.gameObject.SetActive(false);//关闭任务
                });
            }
        }//敌人


        reward.text = "获得:" + taskDataBase.Tasks[id].rewardGoods.goodsType.ToString() + taskDataBase.Tasks[id].rewardGoods.goodsCount;
    }
   
}
