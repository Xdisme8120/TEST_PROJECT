#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):Equips.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):Equips
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equips {
    HeroSystem heroSystem;
    //装备信息
    Dictionary<int,int> itemInfo;
    //装备构造
    public Equips(HeroSystem _heroSystem)
    {
        heroSystem = _heroSystem;
        //初始化字典
        itemInfo = new Dictionary<int, int>();
        for(int i=1;i<=6;i++)
        {
            itemInfo.Add(i,-1);
        }
    }
    //装备栏数据赋值
    public void Init(Dictionary<int,int> _itemInfo)
    {
        for(int i=1;i<=6;i++)
        {
            if(_itemInfo[i]!=-1)
            {
                itemInfo[i] = _itemInfo[i];
                OnEquiped(i,_itemInfo[i]);
            }
        }
    }   
    //移除装备
    public void RemoveEquips(int _gridID)
    {
        OnUnEquiped(_gridID,itemInfo[_gridID]);
    }

    //装上装备
    public void EuqipedEquip(int _gridID,int _equipID)
    {}

    //装备装入时的回调
    void OnEquiped(int _gridID,int _equipID)
    {}

    //装备卸下是的回调
    void OnUnEquiped(int _gridID,int _equipID)
    {}
}