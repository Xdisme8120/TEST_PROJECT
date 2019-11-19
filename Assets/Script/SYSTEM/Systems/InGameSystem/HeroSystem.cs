#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):HeroSystem.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):HeroSystem
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
//英雄系统_存储实时英雄信息
public class HeroSystem : IMainGameSystem
{
    ///数值索引器
    public bool IsControllable
    {
        get { return IsControllable; }
        set { isControllable = value; }
    }
    public bool IsDeath
    {
        get { return isDeath; }
        set { isDeath = value; }
    }
    public bool IsLeader
    {
        get { return isLeader; }
        set { isLeader = value; }
    }
    public bool InLeady
    {
        get { return inLeady; }
        set { inLeady = value; }
    }
    public PhotonView PhotonView
    {
        get { return photonView; }
        set { photonView = value; }
    }
    public PhotonView ReceiveView
    {
        get { return receiveView; }
        set { receiveView = value; }
    }
    public PhotonView ContrastView
    {
        get { return contrastView; }
        set { contrastView = value; }
    }
    //英雄实例
    GameObject hero;
    //英雄信息
    HeroInfo heroInfo;
    //背包信息
    Inventory inventory;
    //装备信息
    Equips equips;
    //英雄是否处于可控状态
    bool isControllable;
    //是否处于死亡状态
    bool isDeath;
    //是否是队长
    bool isLeader;
    //是否在队伍中
    bool inLeady;
    //玩家自己的photonView
    PhotonView photonView;
    //接收用pv
    PhotonView receiveView;
    //对比用pv
    PhotonView contrastView;
    public HeroSystem(InGameSystem _inGameSystem) : base(_inGameSystem)
    { }

    //设置英雄信息
    public void SetHeroInfo(GamingData _data)
    { 

    }

    //初始化英雄系统
    public override void Init()
    {
        heroInfo = new HeroInfo(this);
        inventory = new Inventory(this);
        equips = new Equips(this);
        heroInfo.InitHeroInfo(GamingData.INSTANCE().HeroState);
        inventory.Init(GamingData.INSTANCE().InvenrotyInfo);
        equips.Init(GamingData.INSTANCE().EquipsInfo);
        EventCenter.Broadcast(EventDefine.InitBag, inventory.GetInventoryInfo);
    }
    //英雄系统Update函数的调用
    public override void Update()
    {
        //
    }
    //系统结束回调
    public override void Release()
    {
        //TODO 玩家数据存入GamingData
        GameSystem.Instance.gamingDataController.SaveData(heroInfo.HeroState, inventory.GetInventoryInfo, equips.GetItemInfo);

    }
    /// <summary>
    /// 英雄信息存储TEST
    /// </summary>
    public void SaveData()
    {
        GameSystem.Instance.gamingDataController.SaveData(heroInfo.HeroState, inventory.GetInventoryInfo, equips.GetItemInfo);
    }
    //玩家数值修改
    public void SetATK_DEF(int _type, int _value)
    {
        if (_type == 1)
        {
            heroInfo.HeroState.atk += _value;
        }
        else
        {
            heroInfo.HeroState.def += _value;
        }
    }
    //玩家血量和蓝量的回复
    public void hill(int _type, int _value)
    {
        if (_type == 1)
        {
            heroInfo.HeroState.hp += _value;
        }
        else
        {
            heroInfo.HeroState.sp += _value;
        }
    }
    //玩家受伤并判断死亡
    public void GetDamaged(float _damage)
    {
        heroInfo.HeroState.hp -= _damage;
        if (heroInfo.HeroState.hp <= 0)
        {
            isDeath = true;
        }
    }
    //玩家使用技能(此处只返回是否可以使用并且消耗蓝量)
    public bool UseSkill(float _needValue)
    {
        if (heroInfo.HeroState.sp >= _needValue)
        {
            heroInfo.HeroState.sp -= _needValue;
            return true;
        }
        return false;
    }
    //获得经验
    public void GetExp(int _exp)
    {
        if (heroInfo.HeroState.cueeExp + _exp > heroInfo.HeroState.levelUpExp)
        {
            heroInfo.HeroState.level++;
            _exp -= (heroInfo.HeroState.levelUpExp - heroInfo.HeroState.cueeExp);
            heroInfo.HeroState.levelUpExp = heroInfo.HeroState.level * 100;
            GetExp(_exp);
        }
        else
        {
            heroInfo.HeroState.cueeExp = _exp;
        }

    }
    //任务完成是物品奖励处理
    public void SetTaskReward(QuestRewards _questRewards)
    {
        GetExp(_questRewards.experience);
        inventory.GetItems(_questRewards.Equip);
    }
    //将背包信息发送给剧情系统
    public void SendGridInfo2S()
    {
    
    }

}