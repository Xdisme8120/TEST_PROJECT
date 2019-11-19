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
    //背包信息索引
    public Dictionary<int,GridInfo> GetInventoryInfo
    {
        get{return inventoryInfo;}
    }
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
            inventoryInfo.Add(i, new GridInfo(i,new Item(),0));
        }
    }
    //背包数据赋值
    public void Init(Dictionary<int, GridInfo> _bagInfo)
    {
        //Debug.Log(_bagInfo.Count);
        for (int i = 1; i <= 8; i++)
        {
            if (_bagInfo[i].GetItemID() != -1)
            {
                inventoryInfo[i] = _bagInfo[i];
                //Debug.Log(inventoryInfo[i].item.Name + "--" + inventoryInfo[i].itemCount);
            }
        }
        EventCenter.AddListener<Dictionary<int,GridInfo>>(EventDefine.UI_SendBagInfo,SetBagInfoFromUI);
    }
    //获得物品
    public void GetItem(int _itemID,int _count = 1)
    {
        int t_grid = -1;
        for (int i = 1; i <= inventoryInfo.Count; i++)
        {
            //保存第一个空格
            if (t_grid == -1 && inventoryInfo[i] == null)
            {
                t_grid = i;
            }
            //如果物品栏已存在相应物品则添加
            if (inventoryInfo[i].GetItemID() == _itemID)
            {
                
                inventoryInfo[i].itemCount += _count;

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
        inventoryInfo[t_grid].item = GetItemFromAll(_itemID);
        inventoryInfo[t_grid].itemCount += 1;
        //TODO提示获取物品并修改UI状态

        //向任务系统发送物品信息
        heroSystem.SendGridInfo2S();
    }

    //使用物品
    public void UseItem(int _itemGridID)
    {
        //判断物品是否存在
        if (inventoryInfo[_itemGridID].GetItemID() != -1)
        {
            inventoryInfo[_itemGridID].itemCount--;
            Item item = GamingData.GetItemByID(inventoryInfo[_itemGridID].GetItemID());
            heroSystem.hill(item.UseType,item.Value);
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
    //批量上交材料
    public void SendMaterial(int _itemID, int _count)
    {
        //判断物品是否存在
        for (int i = 1; i <= 8; i++)
        {
            if (inventoryInfo[i].GetItemID() != -1)
            {
                if (inventoryInfo[i].GetItemID() == _itemID && inventoryInfo[i].itemCount >= _count)
                {
                    inventoryInfo[i].itemCount-=_count;
                    //如果物品耗尽
                    if (inventoryInfo[i].itemCount == 0)
                    {

                        ClearGridInfo(i);
                        //TODO给UI发送消息                                             
                    }
                    //否则仅仅减少
                    else
                    {
                        //TODO给UI发送消息
                    }
                }
            }
        }
        //TODO提示物品数量不足
    }
    //交换格子信息
    public void SwitchItem(int _gridID1, int _gridID2)
    {
        Item tempItem = inventoryInfo[_gridID1].item;
        int tempCount = inventoryInfo[_gridID1].itemCount;
        inventoryInfo[_gridID1].item = inventoryInfo[_gridID2].item;
        inventoryInfo[_gridID1].itemCount = inventoryInfo[_gridID2].itemCount;
        inventoryInfo[_gridID2].item = tempItem;
        inventoryInfo[_gridID2].itemCount = tempCount;
    }
    //清空格子信息
    void ClearGridInfo(int _gridID)
    {
        inventoryInfo[_gridID].Reset();
    }
    //设置个格子的信息
    void SetGridInfo(int _gridID, Item _item, int _itemCount = 1)
    {
        inventoryInfo[_gridID].item = _item;
        inventoryInfo[_gridID].itemCount = _itemCount;
    }
    Item GetItemFromAll(int _itemID)
    {
        return GamingData.GetItemByID(_itemID);
    }
    //从UI获得背包信息并输入背包
    public void SetBagInfoFromUI(Dictionary<int, GridInfo> _bagInfo)
    {
        for (int i = 1; i <= 8; i++)
        {
            if (_bagInfo[i].GetItemID() != -1)
            {
                inventoryInfo[i] = _bagInfo[i];
            }
            else
            {
                inventoryInfo[i] = new GridInfo(i,new Item(),0);
            }
        }
    }
    //根据Dic获得物品
    public void GetItems(Dictionary<string,int> _items)
    {
        foreach(var item in _items.Keys)
        {
            GetItem(GamingData.GetItemIDByName(item),_items[item]);
        }
    }
    //根据Dic上交物品
    public void SendItems(Dictionary<string,int> _items)
    {
        foreach(var item in _items.Keys)
        {
            SendMaterial(GamingData.GetItemIDByName(item),_items[item]);
        }
    }
}