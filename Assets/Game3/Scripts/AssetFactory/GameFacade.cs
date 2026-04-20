using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    //单例模式
    public static GameFacade Instance;

    //外观，声明各个模块的对象
    private IAssetFactory assetFactory;

    //添加音频管理模块
    private SoundController soundController;


    private GameFacade() { }

    private void Awake()
    {
        Instance = this;
        Init();
    }
    /// <summary>
    /// 初始化模块
    /// </summary>
    private void Init()
    {
        soundController = GameObject.Find("Controller").GetComponent<SoundController>();
        assetFactory = new ResourcesAssetProxy();

    }

    public GameObject LoadEnemy(string name)
    {
        return assetFactory.LoadEnemy(name);
    }
    public GameObject LoadGlod(string name)
    {
        return assetFactory.LoadGlod(name);
    }
    public GameObject LoadApple(string name)
    {
        return assetFactory.LoadApple(name);
    }
    public GameObject LoadHP(string name)
    {
        return assetFactory.LoadHP(name);
    }
    public GameObject LoadSkill(string name)
    {
        return assetFactory.LoadSkill(name);
    }
    public AudioClip LoadAudioClip(string name)
    {
        return assetFactory.LoadAudioClip(name);
    }

    public void PlaySound(string name)
    {
        soundController.PlaySound(assetFactory.LoadAudioClip(name));
    }

    public void PlaySound2(string name)
    {
        soundController.PlaySound2(assetFactory.LoadAudioClip(name));
    }

    
}
