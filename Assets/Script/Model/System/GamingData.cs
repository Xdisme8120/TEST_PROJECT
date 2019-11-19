using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public enum ItemType
{
    Consumables,//消耗品
    Equip,//装备
    Material//材料
}
public class GamingData
{
    //游戏数据单例
    static GamingData instance;
    //用户名
    public static string username;
    //英雄昵称
    public static string nickname;
    //英雄类型
    public static int heroType;
    //剧情数据
    public static SynData synData;
    //英雄List昵称
    public static string[] heroList;
    //英雄-类型字典
    public static Dictionary<string,int> heroLT;

    public static GamingData INSTANCE()
    {
        if (instance == null)
        {
            instance = new GamingData();
            return instance;
        }
        return instance;
    }
    public GamingData()
    {
        GetIMInfo();
        heroList = new string[2];
        heroLT = new Dictionary<string, int>();
    }
    //数据获取索引器
    public HeroState HeroState
    {
        get { return data_HeroState; }
    }
    public Dictionary<int, GridInfo> InvenrotyInfo
    {
        get { return data_InvenrotyInfo; }
    }
    public Dictionary<int, int> EquipsInfo
    {
        get { return data_EquipsInfo; }
    }
    public SynData SynData
    {
        get { return synData; }
    }
    //英雄状态
    HeroState data_HeroState;
    //背包信息
    Dictionary<int, GridInfo> data_InvenrotyInfo;
    //装备信息
    Dictionary<int, int> data_EquipsInfo;
    //TODO任务状态
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //所有物品信息存储
    static Dictionary<int, Item> itemsInfo;
    //材料信息存储
    //设置英雄状态信息
    public void setHeroState(HeroState _stateData)
    {
        data_HeroState = _stateData;
    }
    //设置背包信息
    public void SetInventoryInfo(Dictionary<int, GridInfo> _invenData)
    {
        data_InvenrotyInfo = _invenData;

    }
    //设置装备信息
    public void SetItemInfo(Dictionary<int, int> _equipsData)
    {
        data_EquipsInfo = _equipsData;
    }
    //设置剧情信息
    public void SetSynData(SynData _synData)
    {
        synData = _synData;
    }
    //获得信息
    public SynData SetItemInfo()
    {
        return synData;
    }
    //初始化物品和材料的信息
    public void GetIMInfo()
    {
        itemsInfo = new Dictionary<int, Item>();

        JsonData allData = GAMETOOLS.GetJson("Item.json");
        JsonData itemInfo = allData["Items"];
        JsonData equipInfo = allData["Equips"];
        JsonData materialInfo = allData["Materials"];
        List<Item> tempItems = JsonMapper.ToObject<List<Item>>(itemInfo.ToJson());
        List<Item> tempEquips = JsonMapper.ToObject<List<Item>>(equipInfo.ToJson());
        List<Item> tempMaterial = JsonMapper.ToObject<List<Item>>(materialInfo.ToJson());
        foreach (var obj in tempItems)
        {
            itemsInfo.Add(obj.ID, obj);
        }
        foreach (var obj in tempEquips)
        {
            itemsInfo.Add(obj.ID, obj);
        }
        foreach (var obj in tempMaterial)
        {
            itemsInfo.Add(obj.ID, obj);
        }
    }
    //根据ID返回物品信息
    public static Item GetItemByID(int _ID)
    {
        //Debug.Log(_ID);
        return itemsInfo[_ID];
    }
    public static int GetItemIDByName(string _string)
    {
        int ID = -1;
        foreach (var item in itemsInfo.Keys)
        {
            if (itemsInfo[item].Name == _string)
            {
                return ID = itemsInfo[item].ID;
            }
        }
        return ID;
    }
}
