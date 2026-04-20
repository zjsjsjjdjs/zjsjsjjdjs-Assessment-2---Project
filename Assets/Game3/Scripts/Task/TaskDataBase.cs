using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GoodsType //任务物品类型
{
    gold,
    apple,
    enemy,
    lev,
}
[Serializable]
public struct TaskGoods
{
    public GoodsType goodsType;
    public int goodsCount;
}
[Serializable]
public struct TaskData
{
    [TextArea]
    public string des;//任务描述
    public int id;//任务ID
    public TaskGoods needGoods;//达成条件
    public TaskGoods rewardGoods;//奖励
}
[CreateAssetMenu(fileName ="NewTaskDataBase",menuName = "CreateNewTaskDataBase/NewTaskDataBase")]
public class TaskDataBase : ScriptableObject
{
    public List<TaskData> Tasks;
}
