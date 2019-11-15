#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):GridInfo.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):GridInfo
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;

public class GridInfo  {
    //GridInfo初始化
    public GridInfo(int _gridID,int _itemID,int _itemCount)
    {
        gridID = _gridID;
        itemID = _itemID;
        itemCount = _itemCount;
    }
    //物品信息初始化
    public void Reset()
    {
        itemID = -1;
        itemCount = 0;
    }
    //格子ID
    public int gridID;
    //物品ID
    public int itemID;
    //物品数量
    public int itemCount; 
}