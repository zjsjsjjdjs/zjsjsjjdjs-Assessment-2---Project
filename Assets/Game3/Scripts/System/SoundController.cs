using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public void PlaySound(AudioClip audio)
    {
        GetComponent<AudioSource>().PlayOneShot(audio);
    }
    public void PlaySound2(AudioClip audio)
    {
        if (GetComponent<AudioSource>().isPlaying)//音频覆盖
        {
            GetComponent<AudioSource>().Stop();
        }

        GetComponent<AudioSource>().PlayOneShot(audio);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
       Application.Quit();
#endif
    }




    public void ButtonPlaySound()
    {
        GameFacade.Instance.PlaySound("UI点击");
    }
    public void ButtonPlaySound3()
    {
        GameFacade.Instance.PlaySound("UI点击2");
    }

    public void ButtonPlaySound2()
    {
        GameFacade.Instance.PlaySound("消息提示");
    }
}
