#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):NpcSystem.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):NpcSystem
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Npc系统
/// </summary>
public class NpcSystem 
{
    #region 属性和字段    

    public SynopsisSystem sy { get; set; }
    public List<Npc> list;
    //************************测试
    private List<string> _npcName = new List<string>() {"村长","情报贩子","铁匠","受伤的骑士长", "手艺人","奸商"};
    private Dictionary<int, int> _npcState = new Dictionary<int, int>();

    #endregion

    #region 方法

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="_sy"></param>
    public NpcSystem(SynopsisSystem _sy)
    {        
        sy = _sy;
        list = new List<Npc>();
        //******************************测试
       
        _npcState.Add(1,1);
        _npcState.Add(2,0);
        _npcState.Add(3,0);
        //InitNpcSystem(_npcState);
    }
    
    /// <summary>
    /// 寻找当前场景里的npc
    /// </summary>
    /// <returns></returns>
    public GameObject[] FindNpc()
    {
        return GameObject.FindGameObjectsWithTag("Npc");
    }

    /// <summary>
    /// 初始化npc，在游戏开始时执行一次
    /// </summary>
    public void InitNpcSystem(Dictionary<int, int> dic)
    {
        FindNpcCom();
        InitNpcID();
        SetNpcState(dic);
    }

    /// <summary>
    /// 为npc动态赋ID和名字
    /// </summary>
    public void InitNpcID()
    {    
        for (int i = 0; i < list.Count; i++)
        {
            list[i].npcID = i + 1;
            list[i].npcName = _npcName[i];
        }
    }
    
    /// <summary>
    /// 拿到全部的Npc脚本
    /// </summary>
    public void FindNpcCom()
    {      
        GameObject[] gameObjects = FindNpc();
        for(int i  = gameObjects.Length - 1 ; i >= 0; i--)
        {
            list.Add(gameObjects[i].GetComponent<Npc>());
            //Debug.Log(list[list.Count - 1].npcName);
        }
    }
    
    /// <summary>
    /// 游戏开始时初始化npc状态，只调用一次
    /// </summary>
    /// <param name="npcState"></param>
    public void SetNpcState(Dictionary<int,int> npcState)
    {
        for(int  i = 0; i < list.Count; i++)
        {
            list[i].SetState((NpcState)npcState[list[i].npcID]);
            //Debug.Log(list[i].NpcState);
        }
    }

    #endregion
}