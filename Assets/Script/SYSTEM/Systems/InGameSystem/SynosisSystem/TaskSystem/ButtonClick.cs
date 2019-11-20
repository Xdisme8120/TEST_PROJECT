#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):ButtonClick.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):ButtonClick
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SUIFW;

public class ButtonClick
{
    #region 属性和字段
    #endregion

    #region 方法

    /// <summary>
    /// 接受任务按钮点击事件
    /// </summary>
    /// <param name="taskSystem"></param>
    /// <param name="bg"></param>
    public void PickUpTask(TaskSystem taskSystem, GameObject bg)
    {
        if (taskSystem.sy.currentTask.taskType != TaskType.Dialogue)
        {
            //TODO 执行遍历背包然后更改进度值的方法
            Debug.Log("待调试");
            EventCenter.Broadcast(EventDefine.FirstCheck);
        }
        taskSystem.SetTaskState(TaskState.Tasking);
        EventCenter.Broadcast(EventDefine.MiniTaskShowC);
        UIManager.GetInstance().CloseUIForms("MainTaskShow");
        SynopsisSystem.isFirst = true;
    }

    /// <summary>
    /// 完成任务按钮点击事件
    /// </summary>
    /// <param name="taskSystem"></param>
    /// <param name="bg"></param>
    public void SubmitTask(TaskSystem taskSystem,GameObject bg)
    {
        //Debug.Log("进行方法绑定");
        taskSystem.sy.SetTalkState(TalkState.Normal);
        //设置当前进入npc状态为普通状态
        taskSystem.sy.currentNpc.SetState(NpcState.Normal);
        if(taskSystem.sy.currentTask.taskType == TaskType.Dialogue)
        {
            taskSystem.sy.npcSystem.list[taskSystem.sy.currentTask.npcID - 1].SetState(NpcState.Normal);
            //拿到对话人的npc脚本
            for (int i = 0; i < taskSystem.sy.npcSystem.list.Count; i++)
            {
                //Debug.Log(npcSystem.list[i].npcName);
                //Debug.Log(currentTask.prop[0]);
                if (taskSystem.sy.npcSystem.list[i].npcName == taskSystem.sy.currentTask.prop[0])
                {
                    taskSystem.sy.npcSystem.list[i].SetState(NpcState.Normal);
                    break;
                }
            }
        }
        else
        {
            //不是对话任务移除采集的道具
            EventCenter.Broadcast(EventDefine.FinishTaskDelete);
        }
        //获取奖励的方法     
        EventCenter.Broadcast(EventDefine.FinishTaskGet);
        
        UIManager.GetInstance().CloseUIForms("MainTaskShow");
        SynopsisSystem.isFirst = true;
        taskSystem.sy.SetTalkState(TalkState.Normal);
        //进行下一个任务的方法
        taskSystem.taskNum += 1;
      
        //点击变成任务刷新状态
        taskSystem.SetTaskState(TaskState.Refresh);
        
        //taskSystem.SetTaskState(TaskState.PickUp);
        EventCenter.Broadcast(EventDefine.ShowUI,taskSystem);
        EventCenter.Broadcast(EventDefine.MiniTaskShowC);
        //小面板显示的替换
    }

    #endregion
}