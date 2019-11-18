#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):ShowNeed.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):ShowNeed
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShowUI
{
    #region 属性和字段

    //静态改成了非静态**********
    public List<string> list = new List<string>();  
    Npc _npc;

    #endregion

    #region 方法

    /// <summary>    
    /// 展示任务需求
    /// </summary>
    public void ShowNeeds(Text need,TaskSystem taskSystem)
    {
        //Debug.Log(taskSystem.GetHashCode());
        need.text = "";
        list.Clear();
        for (int i = 0; i < taskSystem.sy.currentTask.prop.Count; i++)
        {
            list.Add(taskSystem.sy.currentTask.prop[i] + "：");
        }
        //Debug.Log(taskSystem.sy.currentTask.tp.taskComplete["情报贩子"]);
        ShowProgress(taskSystem.sy.currentTask.tp.taskComplete, false);
        ShowProgress(taskSystem.sy.currentTask.td.taskNeed, true);
        for (int i = 0; i < list.Count; i++)
        {        
            need.text += list[i] + "\n";
        }    
    }

    /// <summary>
    /// 展示玩家进度
    /// </summary>
    /// <param name="dic"></param>
    /// <param name="isEnd"></param>
    public void ShowProgress(Dictionary<string, int> dic, bool isEnd)
    {
        int j = 0;
        if (!isEnd)
        {
            foreach (var item in dic)
            {
                list[j] += " " + item.Value + "/";
                j++;
                //****************测试         
                if (j == dic.Count)
                {
                    j = 0;
                }
            }
        }
        else
        {
            foreach (var item in dic)
            {
                list[j] += item.Value;
                j++;
                if (j == dic.Count)
                {
                    j = 0;
                }
            }
        }
 
    }

    /// <summary>
    /// 展示header状态以及button文本
    /// </summary>
    /// <param name="header"></param>
    /// <param name="button"></param>
    public void ShowHeaderAndButton(Text header, Button button,Task currentTask,NpcSystem npcSystem,TalkSystem talkSystem)
    {        
        //如果没到npc处
        if(talkSystem.sy.currentNpc == null)
        {      
            if (button.interactable)
            button.interactable = false;
        }
        //到了npc处
        else
        {
            //Debug.Log(currentTask.taskType);
            //如果是对话类型的任务
            if (currentTask.taskType == TaskType.Dialogue)
            {                
                //拿到对话人的npc脚本
                for (int i = 0; i < npcSystem.list.Count; i++)
                {
                    //Debug.Log(npcSystem.list[i].npcName);
                    //Debug.Log(currentTask.prop[0]);
                    if (npcSystem.list[i].npcName == currentTask.prop[0])
                    {
                        _npc = npcSystem.list[i];
                        break;
                    }                   
                }
                //如果是对话人且对话结束
                if (_npc.npcID == talkSystem.sy.currentNpc.GetComponent<Npc>().npcID &&currentTask.taskState==TaskState.UnSubmit)
                {
                    SetButton(button, true);
                }
                //如果是发布任务的时候
                else if (currentTask.npcID == talkSystem.sy.currentNpc.GetComponent<Npc>().npcID && currentTask.taskState == TaskState.PickUp && talkSystem.sy.TalkState == TalkState.EndTalk)
                {
                    //Debug.Log(3);
                    SetButton(button, true);
                }
                else
                {
                    SetButton(button,false);
                }
              
            }
            //其他任务
            else
            {
                //Debug.Log(5);
                //如果进入的人和发布任务的人一致
                if (currentTask.npcID == talkSystem.sy.currentNpc.GetComponent<Npc>().npcID &&(currentTask.taskState == TaskState.PickUp||currentTask.taskState==TaskState.UnSubmit) && talkSystem.sy.TalkState == TalkState.EndTalk)
                {
                    SetButton(button, true);
                }
                else
                {
                    SetButton(button, false);
                }
            }
        }
        //当
        ShowText(header, button,currentTask);  
    }
    
    /// <summary>
    /// 展示button的文本显示
    /// </summary>
    /// <param name="header"></param>
    /// <param name="button"></param>
    /// <param name="currentTask"></param>
    public void ShowText(Text header, Button button,Task currentTask)
    {
        if (currentTask.taskState == TaskState.PickUp)
        {
            SetText(header, button, "待接取", "接取任务");
        }
        else if (currentTask.taskState == TaskState.Tasking)
        {
            SetText(header, button, "进行中", "领取奖励");
        }
        else if (currentTask.taskState == TaskState.UnSubmit)
        {
            SetText(header, button, "已完成", "领取奖励");
        }
    }
    
    /// <summary>
    /// 设置头文字和button文字
    /// </summary>
    /// <param name="header"></param>
    /// <param name="button"></param>
    /// <param name="headerText"></param>
    /// <param name="buttonText"></param>
    public void SetText(Text header, Button button, string headerText, string buttonText)
    {
        header.text = headerText;
        button.transform.Find("Text").GetComponent<Text>().text = buttonText;    
    }

    /// <summary>
    /// 设置button的交互点击
    /// </summary>
    /// <param name="button"></param>
    /// <param name="isInteratable"></param>
    public void SetButton(Button button , bool isInteratable)
    {
        button.interactable = isInteratable;
    }

    #endregion
}