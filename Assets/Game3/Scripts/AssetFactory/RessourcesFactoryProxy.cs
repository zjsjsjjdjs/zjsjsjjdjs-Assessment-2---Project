using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesAssetProxy : IAssetFactory
{
    //获得工厂对象的引用
    private ResourcesAssetFactory assetFactory = new ResourcesAssetFactory();

    //所有资源需要使用字典存放
    private Dictionary<string, GameObject> mEnemy = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> mGlod = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> mApple = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> mSkill = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> mHP = new Dictionary<string, GameObject>();
    private Dictionary<string, AudioClip> mSound = new Dictionary<string, AudioClip>();


    public AudioClip LoadAudioClip(string name)
    {
        if (mSound.ContainsKey(name))
        {
            return GameObject.Instantiate(mSound[name]);
        }
        else
        {
            AudioClip asset = assetFactory.LoadAudioClip(name);
            mSound.Add(name, asset);
            return GameObject.Instantiate(asset);
        }
    }

    public GameObject LoadEnemy(string name)
    {
        if (mEnemy.ContainsKey(name))
        {
            return GameObject.Instantiate(mEnemy[name]);
        }
        else
        {
            GameObject asset = assetFactory.loadAsset(assetFactory.EnemyPath + name) as GameObject;
            mEnemy.Add(name, asset);
            return GameObject.Instantiate(asset);
        }
    }

    public GameObject LoadGlod(string name)
    {
        if (mGlod.ContainsKey(name))
        {
            return GameObject.Instantiate(mGlod[name]);
        }
        else
        {
            GameObject asset = assetFactory.loadAsset(assetFactory.GlodPath + name) as GameObject;
            mGlod.Add(name, asset);
            return GameObject.Instantiate(asset);
        }
    }

    public GameObject LoadSkill(string name)
    {
        if (mSkill.ContainsKey(name))
        {
            return GameObject.Instantiate(mSkill[name]);
        }
        else
        {
            GameObject asset = assetFactory.loadAsset(assetFactory.SkillPath + name) as GameObject;
            mSkill.Add(name, asset);
            return GameObject.Instantiate(asset);
        }
    }
    public GameObject LoadApple(string name)
    {
        if (mApple.ContainsKey(name))
        {
            return GameObject.Instantiate(mApple[name]);
        }
        else
        {
            GameObject asset = assetFactory.loadAsset(assetFactory.ApplePath + name) as GameObject;
            mApple.Add(name, asset);
            return GameObject.Instantiate(asset);
        }
    }
    public GameObject LoadHP(string name)
    {
        if (mHP.ContainsKey(name))
        {
            return GameObject.Instantiate(mHP[name]);
        }
        else
        {
            GameObject asset = assetFactory.loadAsset(assetFactory.HPPath + name) as GameObject;
            mHP.Add(name, asset);
            return GameObject.Instantiate(asset);
        }
    }
}
