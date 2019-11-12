#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):Inventory.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):Inventory
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Inventory
{
    HeroSystem heroSystem;
    Dictionary<int, GridInfo> inventoryInfo;
    //构造函数
    public Inventory(HeroSystem _heroSystem)
    {
        heroSystem = _heroSystem;
        //初始化背包字典
        inventoryInfo = new Dictionary<int, GridInfo>();
        for (int i = 1; i <= 8; i++)
        {
            inventoryInfo.Add(i, new GridInfo(i, -1, 0));
        }
    }
    //背包数据赋值
    public void Init(Dictionary<int,GridInfo> _bagInfo)
    {
        for (int i = 1; i <= 8; i++)
        {
            if (_bagInfo[i].itemID != -1)
            {
                inventoryInfo[i] = _bagInfo[i];
            }
        }
    }

}