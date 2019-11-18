#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):TaskSystem.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):TaskSystem
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;
using System.Collections.Generic;

public class TaskSystem
{
    #region 属性和字段

    //临时存储对话任务的进度
    Dictionary<string, int> progress;
    //任务的编号
    public int taskNum = 1;
    //json路径
    public string path;   
    //剧情系统
    public SynopsisSystem  sy {get; set;}

    #endregion

    #region 方法

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="_sy"></param>
    public TaskSystem(SynopsisSystem _sy)
    {
        sy = _sy;
        progress = new Dictionary<string, int>();
        EventCenter.AddListener(EventDefine.ChangeDialogueTask,ChangeDialogueTaskState);
        EventCenter.AddListener<Dictionary<string,int>>(EventDefine.ChangeOtherTask,SetChangeAndCheck);
    }

    /// <summary>
    /// 析构
    /// </summary>
    ~TaskSystem()
    {
        EventCenter.RemoveListener(EventDefine.ChangeDialogueTask, ChangeDialogueTaskState);
        EventCenter.RemoveListener<Dictionary<string, int>>(EventDefine.ChangeOtherTask, SetChangeAndCheck);
    }

    /// <summary>
    /// 拿到当前任务的数值并将信息拿到currentTask中
    /// </summary>
    /// <param name="index"></param>
    /// <param name="json"></param>
    public void GetCurrentTask(int index,JsonData json)
    {
        //Debug.Log(1);
        //JsonData json = JsonReader.ReadJson(path);
        sy.currentTask.npcID = (int)json[index.ToString()]["npcID"];
        //Debug.Log(sy.currentTask.npcID);
        //Debug.Log(json[index.ToString()]["npcID"]);
        sy.currentTask.taskType = (TaskType)(int)json[index.ToString()]["taskType"];
        //Debug.Log(sy.currentTask.taskType);
        sy.currentTask.description = (string)json[index.ToString()]["description"];
        //Debug.Log(sy.currentTask.description);      
        sy.currentTask.qr.coinCounts = (int)json[index.ToString()]["qr"]["coinCounts"];
        //Debug.Log(sy.currentTask.qr.coinCounts);
        sy.currentTask.qr.experience = (int)json[index.ToString()]["qr"]["experience"];
        //Debug.Log( sy.currentTask.qr.experience);
        sy.currentTask.qr.Equip = JsonMapper.ToObject<Dictionary<string, int>>(json[index.ToString()]["qr"]["Equip"].ToJson());
        //Debug.Log(sy.currentTask.qr.Equip);
        sy.currentTask.prop = JsonMapper.ToObject<List<string>>(json[index.ToString()]["prop"].ToJson());
        //Debug.Log(sy.currentTask.prop);    
        sy.currentTask.td.taskNeed = JsonMapper.ToObject<Dictionary<string, int>>(json[index.ToString()]["td"].ToJson());
        //Debug.Log(sy.currentTask.td.taskNeed[sy.currentTask.prop[0]]);
        sy.currentTask.tp.taskComplete = JsonMapper.ToObject<Dictionary<string, int>>(json[index.ToString()]["tp"].ToJson());
        //Debug.Log(sy.currentTask.tp.taskComplete[sy.currentTask.prop[0]]);
        sy.currentTask.name = (string)json[index.ToString()]["name"];
        //Debug.Log(sy.currentTask.name);        
    }

    /// <summary>
    ///  读取当前的任务json，每当接到新任务时执行一次
    /// </summary>
    public void GetCurrentTaskAll()
    {       
        string path = Application.streamingAssetsPath + "/TaskTxt/TaskList.json";
        JsonData json = JsonReader.ReadJson(path);
        GetCurrentTask(taskNum, json);
    }

    /// <summary>
    /// 设置当前人物的状态
    /// </summary>
    /// <param name="_taskState"></param>
    public void SetTaskState(TaskState _taskState)
    {
        sy.currentTask.taskState = _taskState;
        //任务状态改变时，改变对应的npc状态
        //任务状态改变时，改变任务的button按钮显示以及状态    
        EventCenter.Broadcast(EventDefine.ShowTaskButtonC);
        EventCenter.Broadcast(EventDefine.ChangeNpcState);
    }

    /// <summary>
    /// 根据进度判断任务状态
    /// </summary>
    /// <returns></returns>
    public void CheckTask()
    {
        int j = 0;
        for(int i = 0; i < sy.currentTask.prop.Count; i++)
        {
            if(sy.currentTask.tp.taskComplete[sy.currentTask.prop[i]]< sy.currentTask.td.taskNeed[sy.currentTask.prop[i]])
            {
                j += 0;             
            }
            else
            {
                //Debug.Log("Plus 1");
                j += 1;
            }
        }
        if (j == sy.currentTask.prop.Count)
        {
            j = 0;
            //未提交状态
            //Debug.Log("Complete but no submit");
            SetTaskState(TaskState.UnSubmit);
        }
        else
        {
            j = 0;
            //返回任务进行中状态
            SetTaskState(TaskState.Tasking);
        }   
    }

    /// <summary>
    /// 改变任务进度
    /// </summary>
    /// <param name="dic"></param>
    public void ChangePropCout(Dictionary<string,int> dic)
    {
        for(int i = 0; i < sy.currentTask.prop.Count; i++)
        {
            //Todo设置传递的数量
            sy.currentTask.tp.taskComplete[sy.currentTask.prop[i]] = dic[sy.currentTask.prop[i]];  
        }  
    }

    /// <summary>
    /// 改变后判断是否满足条件改变状态
    /// </summary>
    /// <param name="dic"></param>
    public void SetChangeAndCheck(Dictionary<string, int> dic)
    {
        ChangePropCout(dic);
        CheckTask();
    }

    /// <summary>
    /// 初始化任务系统,游戏开始时调用一次
    /// </summary>
    /// <param name="taskID"></param>
    /// <param name="_taskState"></param>
    /// <param name="progress"></param>
    public void InitTaskSystem(int taskID,int _taskState, Dictionary<int, GridInfo> dic)
    {
        //设置当前任务ID
        taskNum = taskID;
        //拿到当前任务ID的全部信息
        GetCurrentTaskAll();
        //设置当前任务状态
        sy.currentTask.taskState = (TaskState)_taskState;
        //设置当前任务的进度
        CheckOtherTask(dic);
    }

    /// <summary>
    /// 改变对话任务状态
    /// </summary>
    public void ChangeDialogueTaskState()
    {
        //如果是对话任务
        if(sy.currentTask.taskType == TaskType.Dialogue)
        {
            //Debug.Log("进入正常");
            //当前进入的npc为对话的npc
           if(sy.currentNpc.npcName == sy.currentTask.prop[0])
            {
                //Debug.Log("进入正常");
                progress.Clear();
                //设置对话任务的进度
                progress.Add(sy.currentNpc.npcName, 1);
                SetChangeAndCheck(progress);
                EventCenter.Broadcast(EventDefine.MiniTaskShowC);       
            }
        } 
    }

    /// <summary>
    /// 第一次接取采集类型的任务时进行判断
    /// </summary>
    /// <param name="dic"></param>
    public void CheckOtherTask(Dictionary<int, GridInfo> dic)
    {
        for (int i = 1; i <= dic.Count; i++)
        {
            sy.currentTask.tp.taskComplete[dic[i].item.Name] = dic[i].itemCount;
        }
        CheckTask();
    }

    #endregion
}