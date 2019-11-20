#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):Task.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):Task
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Task
{
    #region 属性和字段

    //Npc编号
    public int npcID;
    //任务编号
    public int taskID;
    //任务类型
    public TaskType taskType;
    //任务名称
    public string name;
    //任务描述
    public string description;
    //任务奖励
    public QuestRewards qr = new QuestRewards();
    //任务需求
    public TaskDemand td = new TaskDemand();
    //任务进度
    public TaskProgress tp = new TaskProgress();
    //任务需求道具
    public List<string> prop = new List<string>();
    //任务状态
    public TaskState taskState = TaskState.Refresh;
    //public TaskState taskState = TaskState.PickUp;

    #endregion
}