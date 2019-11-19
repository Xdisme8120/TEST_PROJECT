using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInfo
{
    HeroSystem heroSystem;
    public  HeroInfo(HeroSystem _heroSystem)
    {
        heroSystem = _heroSystem;
        heroState = new HeroState();
    }
    //基本状态
    HeroState heroState;
    //初始化英雄信息
    public void InitHeroInfo(HeroState _heroState)
    {
        //TODO登陆并成功选择英雄后将英雄的状态初始化
        heroState.hp = _heroState.hp;
        heroState.sp = _heroState.sp;
        heroState.cueeExp = _heroState.cueeExp;
        heroState.level = _heroState.level;
        heroState.atk = _heroState.atk;
        heroState.def = _heroState.def;
        heroState.levelUpExp = _heroState.levelUpExp*100;
    }
    //返回英雄状态信息
    public HeroState HeroState
    {
        get { return heroState; }
    }
    //判断英雄是否死亡
 
}
