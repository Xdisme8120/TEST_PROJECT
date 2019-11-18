#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):TaskPanelShow.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):TaskPanelShow
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using LitJson;

public class TaskPanelShow : MonoBehaviour
{
    #region 属性和字段

    private Text nameTxt;
    private Text description;
    private Text rewards;
    private Text need;  
    private Button button;
    private Text header;
    private GameObject bg;
    ShowUI showUI;
    ButtonClick buttonClick;
    bool isFirst = true;
    //测试
    SynopsisSystem syn;

    #endregion

    #region 生命周期

    private void Awake()
    {
        //测试     
        bg = transform.Find("Bg").gameObject;
        nameTxt = transform.Find("Bg/Text/name").GetComponent<Text>();
        description = transform.Find("Bg/Text/description").GetComponent<Text>();
        rewards = transform.Find("Bg/Text/rewards").GetComponent<Text>();
        need = transform.Find("Bg/Text/needbig").GetComponent<Text>();
        button = transform.Find("Bg/Text/Button").GetComponent<Button>();
        header = transform.Find("Bg/Text/header").GetComponent<Text>();
        showUI = new ShowUI();
        buttonClick = new ButtonClick();     
        EventCenter.AddListener<TaskSystem,NpcSystem,TalkSystem>(EventDefine.ShowTaskButton, ShowButtonAndState);
        EventCenter.AddListener(EventDefine.Init, InitButton);
        EventCenter.AddListener<TaskSystem>(EventDefine.ShowUI, ShowPanel);
        EventCenter.AddListener<SynopsisSystem>(EventDefine.SetTaskPanel, SetTaskPanel);
        EventCenter.AddListener<SynopsisSystem, TalkSystem>(EventDefine.SetTaskPanelEndTalk, ShowTaskPanel);        
    }

    private void Start()
    {
        //测试代码
        InitButton();   
    }

    private void OnDisable()
    {
        EventCenter.RemoveListener<TaskSystem,NpcSystem,TalkSystem>(EventDefine.ShowTaskButton, ShowButtonAndState);     
        EventCenter.RemoveListener(EventDefine.Init, InitButton);
        EventCenter.RemoveListener<TaskSystem>(EventDefine.ShowUI, ShowPanel);
        EventCenter.RemoveListener<SynopsisSystem>(EventDefine.SetTaskPanel, SetTaskPanel);
        EventCenter.RemoveListener<SynopsisSystem, TalkSystem>(EventDefine.SetTaskPanelEndTalk, ShowTaskPanel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //设置任务面板状态
            EventCenter.Broadcast(EventDefine.SetTaskPanelC);
        }
        //当任务面板打开时，进行UI显示
        if (bg.activeSelf && isFirst)
        {
            isFirst = false;
            //显示UI信息         
            EventCenter.Broadcast(EventDefine.ShowUIC);
            //设置button
            EventCenter.Broadcast(EventDefine.ShowTaskButtonC);
        }
        else if(!bg.activeSelf && !isFirst)
        {
            isFirst = true;
        }        
        //EventCenter.Broadcast(EventDefine.Test);        
    }

    #endregion

    #region 方法

    /// <summary>
    /// 任务面板的UI层信息显示
    /// </summary>
    /// <param name="taskSystem"></param>
    public void SetUI(TaskSystem taskSystem)
    {
        rewards.text = "";
        nameTxt.text = taskSystem.sy.currentTask.name;
        description.text = taskSystem.sy.currentTask.description;
        rewards.text += "金钱： " + taskSystem.sy.currentTask.qr.coinCounts + "\n";
        rewards.text += "经验： " + taskSystem.sy.currentTask.qr.experience + "\n";
        rewards.text += "物品： ";
        foreach (var item in taskSystem.sy.currentTask.qr.Equip)
        {
            rewards.text += item.Key + " ";
        }
        //展示任务进度UI
        showUI.ShowNeeds(need, taskSystem);
        //为按钮添加点击事件
        SetButton(taskSystem);        
    }

    /// <summary>
    /// 设置任务面板的激活状态
    /// </summary>
    /// <param name="sy"></param>
    public void SetTaskPanel(SynopsisSystem sy)
    {
        //如果正在npc对话，无法调出任务面板来 
        if (sy.TalkState == TalkState.Talking)
            return;
        bg.SetActive(!bg.activeSelf);
        if (bg.activeSelf)
        {
            //任务面板打开无法进行对话
            sy.IsShowTaskPanel = true;
        }
        else
        {
            sy.IsShowTaskPanel = false;
        }
    }

    /// <summary>
    /// 展示任务面板所有信息
    /// </summary>
    /// <param name="sy"></param>
    /// <param name="taskSystem"></param>
    public void ShowPanel(TaskSystem taskSystem)
    {
        if (taskSystem.sy.currentTask.taskState == TaskState.Refresh)
        {
            taskSystem.GetCurrentTaskAll();
            taskSystem.SetTaskState(TaskState.PickUp);
        }
        SetUI(taskSystem);
    }

    /// <summary>
    /// 展示button的状态和按钮文字
    /// </summary>
    /// <param name="taskSystem"></param>
    /// <param name="npcSystem"></param>
    /// <param name="talkSystem"></param>
    public void ShowButtonAndState(TaskSystem taskSystem,NpcSystem npcSystem,TalkSystem talkSystem)
    {  
        showUI.ShowHeaderAndButton(header, button, taskSystem.sy.currentTask, npcSystem,talkSystem);
    }

    /// <summary>
    /// 初始化button的按钮交互
    /// </summary>
    public void InitButton()
    {
        button.interactable = false;
    }

    /// <summary>
    /// 设置button绑定事件
    /// </summary>
    /// <param name="taskSystem"></param>
    public void SetButton(TaskSystem taskSystem)
    {
        if(taskSystem.sy.currentTask.taskState == TaskState.PickUp)
        {    
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(delegate () { buttonClick.PickUpTask(taskSystem,bg); });
        }  
        else if(taskSystem.sy.currentTask.taskState == TaskState.UnSubmit)
        {     
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(delegate () { buttonClick.SubmitTask(taskSystem,bg); });
        } 
    }

   /// <summary>
   /// 对话结束展示任务面板
   /// </summary>
   /// <param name="bg"></param>
    public void ShowTaskPanel(SynopsisSystem sy,TalkSystem talkSystem)
    {
        sy.IsShowTaskPanel = true;
        //TODO 实现任务面板的初始化
        if (sy.TalkState == TalkState.EndTalk)
        {
            if (sy.currentNpc.NpcState == NpcState.HasTask)
            {
                //Debug.Log(1);
                bg.SetActive(true);             
            }
            else if (sy.currentNpc.NpcState == NpcState.FinishTask)
            {
                bg.SetActive(true);           
            }
        }
    } 

    #endregion
}