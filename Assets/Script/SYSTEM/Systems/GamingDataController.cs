#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):GamingDataController.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):GamingDataController
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.Json;
public class GamingDataController : BaseSystemController
{
    //数据实体
    GamingData data;
    public GamingDataController(GameSystem _system) : base(_system)
    {
        //实例化数据实体
        data = GamingData.INSTANCE();
    }
    //初始化数据
    public void InitData(JsonData _data)
    {
        Debug.Log(Time.time);
        JsonData json_HeroState = _data["charInfo"];
        data.setHeroState(GetHeroStateData(JsonMapper.ToObject(json_HeroState.ToJson())));
        JsonData json_bagInfo = _data["bagInfo"];
        data.SetInventoryInfo(GetInventoryData(JsonMapper.ToObject(json_bagInfo.ToJson())));
        JsonData json_itemInfo = _data["itemInfo"];
        data.SetItemInfo(GetItemData(JsonMapper.ToObject(json_itemInfo.ToJson())));

    }
    //存储数据
    public void SaveData()
    { }
    //获取英雄状态数据
    public HeroState GetHeroStateData(JsonData _data)
    {
        
        HeroState tempState = new HeroState();
        tempState.hp = int.Parse((string)_data["Hp"]);
        tempState.sp = int.Parse((string)_data["Mp"]);
        tempState.cueeExp = int.Parse((string)_data["CurrExp"]);
        tempState.level = int.Parse((string)_data["level"]);
        return tempState;

    }
    //返回背包信息
    public Dictionary<int, GridInfo> GetInventoryData(JsonData _data)
    {
        Dictionary<int, GridInfo> tempInventory = new Dictionary<int, GridInfo>();
        for (int i = 1; i <= 8; i++)
        {
            string tempData = (string)_data["bagItem" + i];
            if (tempData != "-1")
            {
                string[] arrays = tempData.Split('|');
                tempInventory.Add(i, new GridInfo(i, GamingData.GetItemByID(int.Parse(arrays[0])), int.Parse(arrays[1])));
            }
            else
            {
                tempInventory.Add(i, new GridInfo(i, null, 0));
            }
        }
        return tempInventory;
    }
    //返回装备信息
    public Dictionary<int, int> GetItemData(JsonData _data)
    {
        Dictionary<int, int> tempItemInfo = new Dictionary<int, int>();
        for (int i = 1; i <= 6; i++)
        {
            int tempID =int.Parse((string)_data["weapon"+i]);
            if (tempID != -1)
            {
                tempItemInfo.Add(i, tempID);
            }
            else
            {
                tempItemInfo.Add(i, -1);
            }
        }
        return tempItemInfo;
    }
}
/*
{
    "errorcode":0,
    "charInfo":{
        "heroName":"bitch",
        "level":null,
        "CurrExp":null,
        "Hp":null,
        "Mp":null
    },
    "itemInfo":{
        "heroName":"bitch",
        "weapon1":"-1",
        "weapon2":"-1",
        "weapon3":"-1",
        "weapon4":"-1",
        "weapon5":"-1",
        "weapon6":"-1"
    },
    "bagInfo":{
        "heroName":"bitch",
        "bagItem1":"-1",
        "bagItem2":"-1",
        "bagItem3":"-1",
        "bagItem4":"-1",
        "bagItem5":"-1",
        "bagItem6":"-1",
        "bagItem7":"-1",
        "bagItem8":"-1"
    }
}*/
