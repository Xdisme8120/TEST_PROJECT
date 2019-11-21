#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):GameSystem.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):GameSystem
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 剧情系统
/// </summary>
public class SynopsisSystem : IMainGameSystem
{
    #region 属性和字段

    public Task currentTask;
    public Npc currentNpc = null;
    public TaskSystem taskSystem;
    public NpcSystem npcSystem;
    public TalkSystem talkSystem;
    public InGameSystem _inGameSystem { get; set; }
    public static bool isFirst = true;

    //对话状态
    private TalkState talkState = TalkState.Normal;
    //是否在显示任务面板
    public bool IsShowTaskPanel;

    #endregion

    #region 属性访问器

    public TalkState TalkState
    {
        get
        {
            return talkState;
        }
    }

    #endregion

    #region 方法

    /// <summary>
    /// 构造方法
    /// </summary>
    public SynopsisSystem(InGameSystem _inGame) : base(_inGame)
    {
        _inGameSystem = _inGame;
        EventCenter.AddListener(EventDefine.ShowUIC, ShowUI);
        EventCenter.AddListener(EventDefine.ShowTalkTextC, ShowTalk);
        EventCenter.AddListener(EventDefine.ChangeNpcState, SetNpcState);
        EventCenter.AddListener(EventDefine.ShowTaskButtonC, ShowButton);
        EventCenter.AddListener(EventDefine.SetTaskPanelC, ShowTaskPanel);
        EventCenter.AddListener(EventDefine.MiniTaskShowC, ShowMiniTaskPanel);
        EventCenter.AddListener(EventDefine.Test, ShowProgress);
        taskSystem = new TaskSystem(this);
        npcSystem = new NpcSystem(this);
        talkSystem = new TalkSystem(this);
        currentTask = new Task();
        //currentTask.taskState = TaskState.PickUp;
    }

    /// <summary>
    /// 析构
    /// </summary>
    ~SynopsisSystem()
    {
        EventCenter.RemoveListener(EventDefine.ShowUIC, ShowUI);
        EventCenter.RemoveListener(EventDefine.ShowTalkTextC, ShowTalk);
        EventCenter.RemoveListener(EventDefine.ChangeNpcState, SetNpcState);
        EventCenter.RemoveListener(EventDefine.ShowTaskButtonC, ShowButton);
        EventCenter.RemoveListener(EventDefine.SetTaskPanelC, ShowTaskPanel);
        EventCenter.RemoveListener(EventDefine.MiniTaskShowC, ShowMiniTaskPanel);
        EventCenter.RemoveListener(EventDefine.Test, ShowProgress);
    }

    /// <summary>
    /// 当任务状态发生改变时，改变npc状态
    /// </summary>
    public void SetNpcState()
    {
        Npc target = null;
        Task task = currentTask;
        //如果是对话类型的任务，设置需要对话的npc为即将对话状态
        if (task.taskType == TaskType.Dialogue)
        {
            for (int i = 0; i < npcSystem.list.Count; i++)
            {
                if (npcSystem.list[i].npcName == task.prop[0])
                {
                    target = npcSystem.list[i];
                    break;
                }
            }
        }
        else
        {
            target = null;
        }
        if (target != null)
        {
            switch (task.taskState)
            {
                //待领取状态不变
                case TaskState.PickUp:
                    break;
                //任务进行中状态，设置为将要对话状态
                case TaskState.Tasking:
                    target.SetState(NpcState.WillTalk);
                    break;
                //对话完成时，设置为未提交状态，设置为完成任务状态
                case TaskState.UnSubmit:
                    target.SetState(NpcState.FinishTask);
                    break;
                //任务提交时，显示下一个可以接取的任务，显示下一个可以接取的任务
                case TaskState.Completed:
                    target.SetState(NpcState.Normal);
                    break;
                default:
                    break;
            }
        }
        //设置任务发起者的npc状态
        switch (task.taskState)
        {
            //待接取状态时，设置为有任务状态
            case TaskState.PickUp:
                npcSystem.list[task.npcID - 1].SetState(NpcState.HasTask);
                //Debug.Log(npcSystem.list[0].npcName);
                //Debug.Log(npcSystem.list[task.npcID - 1].npcName  + "执行了");
                break;
            //当任务进行中状态时，设置发起者为进行中状态
            case TaskState.Tasking:
                npcSystem.list[task.npcID - 1].SetState(NpcState.Tasking);
                break;
            //当任务未提交时，设置为完成任务状态
            case TaskState.UnSubmit:
                npcSystem.list[task.npcID - 1].SetState(NpcState.FinishTask);
                break;
            //当任务完成时，设置为普通状态
            case TaskState.Completed:
                npcSystem.list[task.npcID - 1].SetState(NpcState.Normal);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 设置对话状态
    /// </summary>
    /// <param name="_talkState"></param>
    public void SetTalkState(TalkState _talkState)
    {
        talkState = _talkState;
    }

    /// <summary>
    /// 二次广播展示UI
    /// </summary>
    public void ShowUI()
    {
        //Debug.Log(0);
        EventCenter.Broadcast(EventDefine.ShowUI, taskSystem);
    }

    /// <summary>
    /// 二次广播展示对话
    /// </summary>
    public void ShowTalk()
    {
        //******************测试
        //Debug.Log(talkState);
        //Debug.Log(IsShowTaskPanel);
        if (!IsShowTaskPanel)
        {
            //Debug.Log(talkState);
            EventCenter.Broadcast(EventDefine.ShowTalkText, this, talkSystem);
        }
    }

    /// <summary>
    /// 二次广播设置button
    /// </summary>
    public void ShowButton()
    {
        //Debug.Log(2);
        EventCenter.Broadcast(EventDefine.ShowTaskButton, taskSystem, npcSystem, talkSystem);
    }

    /// <summary>
    /// 二次广播开启task面板
    /// </summary>
    public void ShowTaskPanel()
    {
        EventCenter.Broadcast(EventDefine.SetTaskPanel, this);
    }

    /// <summary>
    /// 二次广播更新小任务面板
    /// </summary>
    public void ShowMiniTaskPanel()
    {
        EventCenter.Broadcast(EventDefine.MiniTaskShow, taskSystem);
    }

    /// <summary>
    /// Show进度**************测试
    /// </summary>
    public void ShowProgress()
    {
    }


    #endregion

    #region 继承的生命周期

    /// <summary>
    /// 剧情系统的初始化
    /// </summary> 
    public override void Init()
    {
        Debug.Log("吃吃吃屎");
        npcSystem.InitNpcSystem(GamingData.synData.npcState);
        taskSystem.InitTaskSystem(GamingData.synData.taskID, GamingData.synData.taskState, GamingData.INSTANCE().InvenrotyInfo);
        ShowMiniTaskPanel();
        //TODO 任务系统初始化  
        //拿到当前的任务编号，拿到当前任务，将当前任务的初值赋值进去，根据任务编号初始化npc状态
        //TODO 对话系统初始化
    }
    /// <summary>
    /// 游戏结束时存储剧情系统数据
    /// </summary>
    public void SaveSynData()
    {
        SynData synData = new SynData();
        synData.taskID = taskSystem.taskNum;
        Debug.Log(currentTask.taskID+"sdadad");
        synData.TaskName = currentTask.name;
        synData.taskState = (int)currentTask.taskState;
        for (int i = 0; i < npcSystem.list.Count; i++)
        {
            synData.npcState.Add(npcSystem.list[i].npcID, (int)npcSystem.list[i].NpcState);
        }
        GamingData.INSTANCE().SetSynData(synData);
    }
    public override void Update()
    {
        //throw new System.NotImplementedException();
    }

    public override void Release()
    {
        SaveSynData();
    }

    #endregion
}