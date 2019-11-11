using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamingData
{
    //游戏数据单例
    static GamingData instance;
    public static GamingData INSTANCE()
    {
        if (instance == null)
        {
            instance = new GamingData();
            return instance;
        }
        return instance;
    }
//数据获取索引器
    public HeroState HeroState
    {
        get{return data_HeroState;}
    }
    public Dictionary<int, GridInfo> InvenrotyInfo
    {
        get{return data_InvenrotyInfo;}
    }
    public Dictionary<int, int> EquipsInfo
    {
        get{return data_EquipsInfo;}
    }
    //英雄状态
    HeroState data_HeroState;
    //背包信息
    Dictionary<int, GridInfo> data_InvenrotyInfo;
    //装备信息
    Dictionary<int, int> data_EquipsInfo;
    //TODO任务状态
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


}
