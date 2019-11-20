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

public class Equips
{
    public Dictionary<int, int> GetItemInfo
    {
        get { return itemInfo; }
    }
    public Dictionary<int, GridInfo> GetEquipsInfo
    {
        get { return EquipInfo; }
    }
    HeroSystem heroSystem;
    //装备信息
    Dictionary<int, int> itemInfo;
    Dictionary<int, GridInfo> EquipInfo;
    //装备构造
    public Equips(HeroSystem _heroSystem)
    {
        heroSystem = _heroSystem;
        //初始化字典
        itemInfo = new Dictionary<int, int>();
        for (int i = 1; i <= 6; i++)
        {
            itemInfo.Add(i, -1);
        }
    }
    //装备栏数据赋值
    public void Init(Dictionary<int, int> _itemInfo)
    {
        for (int i = 1; i <= 6; i++)
        {
            //如果当前装备栏有装备
            if (itemInfo[i] != -1)
            {
                //如果装备相同
                if (itemInfo[i] == _itemInfo[i])
                {
                    continue;
                }
                //如果装备不同
                //如果现在该格子没有装备
                if (_itemInfo[i] == -1)
                {

                    RemoveEquips(i);
                    continue;
                }
                //否则移除就装备装入新装备
                RemoveEquips(i);
                itemInfo[i] = _itemInfo[i];
                EuqipedEquip(i, _itemInfo[i]);
            }
            //如果没有装备
            else
            {
                //如果现在的格子有装备
                if (_itemInfo[i] != -1)
                {
                    //装上改装备
                    EuqipedEquip(i, _itemInfo[i]);
                    continue;
                }
                //如果现在的格子没有装备
                {
                    continue;
                }
            }

        }
    }
    //移除装备
    public void RemoveEquips(int _gridID)
    {
        OnUnEquiped(_gridID, itemInfo[_gridID]);
        itemInfo[_gridID] = -1;
        Debug.Log(_gridID + "___" + itemInfo[_gridID]);
    }

    //装上装备
    public void EuqipedEquip(int _gridID, int _equipID)
    {
        OnEquiped(_gridID, _equipID);
        itemInfo[_gridID] = _equipID;
        Debug.Log(_gridID + "___" + itemInfo[_gridID]);
    }

    //装备装入时的回调
    void OnEquiped(int _gridID, int _equipID)
    {
        Item item = GamingData.GetItemByID(_equipID);
        heroSystem.SetATK_DEF(item.UseType, item.Value);
    }

    //装备卸下是的回调
    void OnUnEquiped(int _gridID, int _equipID)
    {
        Item item = GamingData.GetItemByID(_equipID);
        heroSystem.SetATK_DEF(item.UseType, -item.Value);
    }

    //获得当前装备信息并同步给UI(仅仅在初始化的时候调用)
    public void GetEquipInfo()
    {
        EquipInfo = new Dictionary<int, GridInfo>();
        for (int i = 1; i <= 6; i++)
        {
            if (itemInfo[i] != -1)
            {
                EquipInfo.Add(i, new GridInfo(i, GamingData.GetItemByID(itemInfo[i]), 1));
            }
            else
            {
                EquipInfo.Add(i, new GridInfo(i, new Item(), 1));
            }
        }
        EventCenter.Broadcast(EventDefine.InitEquip, EquipInfo);
    }
}