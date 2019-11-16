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
    //TODO获取所有物品Json信息
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
            //inventoryInfo.Add(i, new GridInfo(i, , 0));
        }
    }
    //背包数据赋值
    public void Init(Dictionary<int, GridInfo> _bagInfo)
    {
        for (int i = 1; i <= 8; i++)
        {
            if (_bagInfo[i].item.ID != -1)
            {
                inventoryInfo[i] = _bagInfo[i];
            }
        }
    }
    //获得物品
    public void GetItem(int _itemID)
    {

        int t_grid = -1;
        for (int i = 0; i < inventoryInfo.Count; i++)
        {
            //保存第一个空格
            if (t_grid == -1 && inventoryInfo[i].item.ID == -1)
            {
                t_grid = inventoryInfo[i].gridID;
            }
            //如果物品栏已存在相应物品则添加
            if (inventoryInfo[i].item.ID == _itemID)
            {
                inventoryInfo[i].itemCount += 1;

                //TODO提示获取物品并发送消息修改UI
                return;
            }
        }
        if (t_grid == -1)
        {
            //TODO 提示物品栏已满

            return;
        }

        //否则在新格子里添加
        inventoryInfo[t_grid].item.ID = _itemID;
        inventoryInfo[t_grid].itemCount += 1;
        //TODO提示获取物品并修改UI状态
    }
    //使用物品
    public void UseItem(int _itemGridID)
    {
        //判断物品是否存在
        if (inventoryInfo[_itemGridID].item.ID != -1)
        {
            inventoryInfo[_itemGridID].itemCount--;
            //如果物品耗尽
            if (inventoryInfo[_itemGridID].itemCount == 0)
            {

                ClearGridInfo(_itemGridID);
                //TODO给UI发送消息                                             
            }
            else
            {

                //TODO提示使用物品并实现效果
            }

        }
        else
        {
            return;
        }
    }
    //交换格子信息
    public void SwitchItem(int _gridID1, int _gridID2)
    {
        GridInfo temp = inventoryInfo[_gridID1];
        inventoryInfo[_gridID1] = inventoryInfo[_gridID2];
        inventoryInfo[_gridID2] = temp;
    }
    //清空格子信息
    void ClearGridInfo(int _gridID)
    {
        inventoryInfo[_gridID].Reset();
    }
    //设置个格子的信息
    void SetGridInfo(int _gridID, int _itemID, int _itemCount = 1)
    {
        inventoryInfo[_gridID].item.ID = _itemID;
        inventoryInfo[_gridID].itemCount = _itemCount; 
    }
}