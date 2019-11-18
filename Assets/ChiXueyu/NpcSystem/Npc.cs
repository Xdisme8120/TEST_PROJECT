#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):Npc.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):Npc
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Npc : MonoBehaviour
{
    #region 属性和字段

    //TalkSystem talkSystem;
    //名字
    public string npcName;
    //npc ID属性
    public int npcID;
    //npc 状态属性
    private NpcState npcState = NpcState.Normal;    

    #endregion

    #region 属性访问器

    public int NpcID
    {
        get
        {
            return npcID;
        }
        set
        {
            npcID = value;
        }
    }

    public NpcState NpcState
    {
        get
        {
            return npcState;
        }      
        
    }

    public Npc(int _npcID)
    {
        npcID = _npcID;
    }

    #endregion

    #region 生命周期

    private void Awake()
    {       
       
      
    }
    //Test
    private void Start()
    {
 
    }

    #endregion

    #region 触发器逻辑

    /// <summary>
    /// 进入触发器时，将当前npc设置为交互npc
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        EventCenter.Broadcast(EventDefine.SetCurrentNpc, this);
        //Debug.Log(TalkSystem.currentNpc.NpcID);
    }

    /// <summary>
    /// 按下E键后，触发对话事件
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EventCenter.Broadcast(EventDefine.ShowTalkTextC);
        }
    }

    /// <summary>
    /// 离开触发器，将当前交互的npc重置
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {   
        //设置当前npc为null
        EventCenter.Broadcast(EventDefine.SetCurrentNpcNull);   
    }

    #endregion

    #region 方法

    /// <summary>
    /// 设置当前npc状态
    /// </summary>
    /// <param name="npcState"></param>
    public void SetState(NpcState _npcState)
    {
        npcState = _npcState;
    }

    #endregion
}