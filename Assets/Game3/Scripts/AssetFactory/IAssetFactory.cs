using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//工厂模式的父类接口
public interface IAssetFactory
{

    AudioClip LoadAudioClip(string name);
    GameObject LoadSkill(string name);
    GameObject LoadGlod(string name);
    GameObject LoadApple(string name);
    GameObject LoadHP(string name);
    GameObject LoadEnemy(string name);

}
