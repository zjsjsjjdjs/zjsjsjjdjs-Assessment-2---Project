using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodBar : MonoBehaviour
{
    public Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void  SetMaxBlood(int maxBlood)
    {
        slider.maxValue = maxBlood;
        slider.value = maxBlood;
    }
     public void SetUpdate(int bloodUpdate)
    {
        slider.value = bloodUpdate;
    }
   
}
