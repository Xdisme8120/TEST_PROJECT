using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInfo
{
    //基本状态
    HeroState heroState;
    //背包
    Dictionary<int,GridInfo> InvenrotyInfo;
    //装备
    Dictionary<int,int> EquipsInfo;
    //职业
    HeroType heroType;

    //初始化英雄信息
    public void InitHeroInfo(GamingData _gameingData)
    {
        //TODO登陆并成功选择英雄后将英雄的状态初始化
    }
    //返回英雄状态信息
    public HeroState HeroState
    {
        get { return heroState; }
    }
    //返回背包信息

}
