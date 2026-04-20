using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventRole
{
   NONE,
   GAMEOVER
}
public delegate void callBack();
public class EventCenter : MonoBehaviour
{   
    private static Dictionary<EventRole, Delegate> mEvent = new Dictionary<EventRole, Delegate>();
    private static void OnListenerAdding(EventRole eventRole,callBack call)
    {
        if (!mEvent.ContainsKey(eventRole))
        {
            mEvent.Add(eventRole,null);

        }

    }
    public static void OnListnerRemoved(EventRole eventRole)
    {
        if (mEvent[eventRole] == null)
        {
          mEvent.Remove(eventRole);
        }
    }
    public static void AddListener(EventRole eventRole,callBack call)
    {
        OnListenerAdding(eventRole, call);
        mEvent[eventRole] = (callBack)mEvent[eventRole] + call;
    }
    public static void RemoveListener(EventRole eventRole,callBack call)
    {
        mEvent[eventRole] = (callBack)mEvent[eventRole] - call;
        OnListnerRemoved(eventRole);
    } 
    public static void Broadcast(EventRole eventRole)
    {
        Delegate d;

        if (mEvent.TryGetValue(eventRole,out d))
        {
         callBack callBack=(callBack)d;
            if(callBack != null)
            {
                callBack();
            }
            else {
                Debug.LogWarning("事件不存在");
         throw new Exception("事件不存在");
            }
        }
        
    }
}
