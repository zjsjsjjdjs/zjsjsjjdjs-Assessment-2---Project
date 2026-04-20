using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesAssetFactory : IAssetFactory
{
    public string AuidoPath = "Sound/";
    public string SkillPath = "Skill/";
    public string GlodPath = "Object/";
    public string ApplePath = "Object/";
    public string HPPath = "Object/";
    public string EnemyPath = "Enemy/";

    public GameObject LoadEnemy(string name)
    {
        return InstantiateGameObject(EnemyPath + name);

    }

    public AudioClip LoadAudioClip(string name)
    {
        return Resources.Load<AudioClip>(AuidoPath + name);
    }

    public GameObject LoadSkill(string name)
    {
        return InstantiateGameObject(SkillPath + name);
    }

    public GameObject LoadGlod(string name)
    {
        return InstantiateGameObject(GlodPath + name);
    }

    public GameObject LoadApple(string name)
    {
        return InstantiateGameObject(ApplePath + name);
    }
    public GameObject LoadHP(string name)
    {
        return InstantiateGameObject(HPPath + name);
    }




    //模板 生成资源对象
    public GameObject InstantiateGameObject(string path)
    {
        Object o = Resources.Load<GameObject>(path);
        if (o == null)
        {
            Debug.LogError("无法加载资源，路径" + path);
        }
        return GameObject.Instantiate(o) as GameObject;
    }

    //生成资产
    public Object loadAsset(string path)
    {
        Object o = Resources.Load<GameObject>(path);
        if (o == null)
        {
            Debug.LogError("无法加载资源，路径" + path);
        }
        return o;
    }


}
