#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):TalkSystem.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):TalkSystem
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 对话系统
/// </summary>
public class TalkSystem
{
    #region 属性和字段

    public SynopsisSystem sy { get; set; }
    //当前进行的对话序号
    public int index = 1;

    public SaveTalkTxt Ins = SaveTalkTxt.GetInstance(); 

    #endregion

    #region 方法

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="_sy"></param>
    public TalkSystem(SynopsisSystem _sy)
    {
        sy = _sy;
        EventCenter.AddListener<Npc>(EventDefine.SetCurrentNpc, SetCurrentNpc);
        EventCenter.AddListener(EventDefine.SetCurrentNpcNull, SetCurrentNpc);
    }

    /// <summary>
    /// 析构函数
    /// </summary>
    ~TalkSystem()
    {
        EventCenter.RemoveListener<Npc>(EventDefine.SetCurrentNpc, SetCurrentNpc);
        EventCenter.RemoveListener(EventDefine.SetCurrentNpcNull, SetCurrentNpc);
    }

    /// <summary>
    /// 初始化当前对话存储库
    /// </summary>
    public void InitTalk()
    {
        index = 1;
        Ins.dic.Clear();
    }

    /// <summary>
    /// 展示对话
    /// </summary>
    /// <returns></returns>
    public ShowTxt ShowTxts()
    {
        if (index <= Ins.dic.Count)
        {
            //测试         
            return Ins.dic[index++];
        }
        else
        {
            TalkCallBack.talkBg.SetActive(false);
            TalkCallBack.isFirst = true;
            index = 1;
            //设置当前为结束npc对话状态
            sy.SetTalkState(TalkState.EndTalk);      
            //如果当前任务是结束对话状态，进行对话任务的判断
            EventCenter.Broadcast(EventDefine.ChangeDialogueTask);  

            //打开任务面板
            EventCenter.Broadcast(EventDefine.SetTaskPanelEndTalk, sy, this);
            //Debug.Log(sy.currentTask.tp.taskComplete["情报贩子"]);
            return null;
        }
        //TODO 实现任务对话的展示
        
    }

    /// <summary>
    /// 初始化对话系统
    /// </summary>
    public void Init()
    {
        //初始化button
        EventCenter.Broadcast(EventDefine.Init);
    }

    /// <summary>
    /// 设置当前npc为进入触发器的npc
    /// </summary>
    /// <param name="_npc"></param>
    public void SetCurrentNpc(Npc _npc)
    {
        sy.currentNpc = _npc;    
    }

    /// <summary>
    /// 退出时设置npc为null
    /// </summary>
    public void SetCurrentNpc()
    {
        sy.currentNpc = null;
        sy.IsShowTaskPanel = false;
    }    

    #endregion
}