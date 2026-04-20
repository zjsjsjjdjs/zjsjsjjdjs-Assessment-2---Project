using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 游戏管理 用于敌人子弹对象池管理 积分管理 菜单面板管理
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject MonsterBulletPrefab; // 敌人子弹预制体
    public GameObject MonsterBulletExpPrefab; // 敌人子弹碰撞后播放的特效预制体
    
    public ObjectPool<GameObject> MonsterBulletPool; // 敌人子弹对象池
    public ObjectPool<GameObject> MonsterBulletExpPool; // 子弹碰撞后播放的粒子特效对象池

    public Text CountText; // 积分用的Text组件
    private int Count; // 分数

    public RectTransform MenuPanelRect; // 菜单面板的Transform
    public Vector3 OpenPos; // 菜单打开时的坐标
    public Vector3 ClosePos; // 菜单关闭时的坐标
    [Range(0,1)]
    public float t; // 菜单打开关闭时移动过渡的系数

    private bool isClose = true; // 菜单是否为关闭
    private Coroutine menuAnim; // 菜单打开关闭的协程动画
    private void Awake()
    {
        Instance = this;
        MonsterBulletPool = new ObjectPool<GameObject>(createBulletFunc, actionOnBulletGet, actionOnBulletRelease, actionOnBulletDestroy);
        MonsterBulletExpPool = new ObjectPool<GameObject>(createBulletExpFunc, actionOnBulletGet, actionOnBulletRelease, actionOnBulletDestroy);

    }

    // Start is called before the first frame update
    void Start()
    {
     
        CountText.text = 0.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ToggleMenu(bool state) // 打开或关闭菜单
    {
        isClose = state;
        if (menuAnim != null)
        {
            StopCoroutine(menuAnim);
        }
        menuAnim = StartCoroutine(OpenOrCloseMenuPanel(!isClose)); // 启用协程

        if (!isClose) // 如果是打开菜单则暂停游戏 显示鼠标
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else // 如果是关闭菜单则恢复游戏 并且锁定鼠标
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }
    
    // 需填入一个带T类型返回值的方法，即自定义新建对象时的操作
    public GameObject createBulletFunc()
    {
        GameObject obj = Instantiate(MonsterBulletPrefab);
        return obj;
    }
    
    public GameObject createBulletExpFunc()
    {
        GameObject obj = Instantiate(MonsterBulletExpPrefab);
        return obj;
    }
    void actionOnBulletGet(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    void actionOnBulletRelease(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    // 池满销毁
    void actionOnBulletDestroy(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    public void AddCount(int i = 1)
    {
        Count += i;
        CountText.text = Count.ToString();
    }

    public void GotoStartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }
    
    // 菜单动画
    IEnumerator OpenOrCloseMenuPanel(bool isOpen)
    {
        Vector3 targetPos = isOpen ? OpenPos : ClosePos;
        if (isOpen)
        {
            MenuPanelRect.gameObject.SetActive(true);
        }
        while (Vector3.Distance(MenuPanelRect.position, targetPos) > 0.01)
        {
            MenuPanelRect.anchoredPosition = Vector3.Lerp(MenuPanelRect.anchoredPosition, targetPos, t);
            yield return null;
        }
        if (!isOpen)
        {
            MenuPanelRect.gameObject.SetActive(false);
        }
        menuAnim = null;
    }
}
