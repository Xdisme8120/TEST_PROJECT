#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):QuestRewards.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):QuestRewards
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestRewards
{
    #region

    //金币奖励
    public int coinCounts;
    //经验奖励
    public int experience;
    //道具奖励
    public Dictionary<string, int> Equip = new Dictionary<string, int>();

    #endregion
}