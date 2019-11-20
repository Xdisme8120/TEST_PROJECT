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
using SUIFW;
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
    //是否打开背包
    bool isOpenInventory;
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
    { }
    //初始化英雄系统
    public override void Init()
    {
        heroInfo = new HeroInfo(this);
        inventory = new Inventory(this);
        equips = new Equips(this);
        heroInfo.InitHeroInfo(GamingData.INSTANCE().HeroState);
        inventory.Init(GamingData.INSTANCE().InvenrotyInfo);
        equips.Init(GamingData.INSTANCE().EquipsInfo);
        EventCenter.AddListener(EventDefine.FinishTaskDelete, DeleteItems);
        EventCenter.AddListener(EventDefine.FinishTaskGet, GetItems);

        EventCenter.AddListener(EventDefine.UI_SetInventoryC, SetInventoryUI);
        EventCenter.AddListener(EventDefine.UI_SetPlayerInfo2InvenC, SetPlayerInfo2Inven);

        EventCenter.AddListener(EventDefine.InitEquipC, SetEquipInfo2Inven);
        EventCenter.AddListener<Dictionary<int, int>>(EventDefine.UI_SendEquipInfo, GetEquips);
        EventCenter.Broadcast(EventDefine.InitBag, inventory.GetInventoryInfo);

        UIManager.GetInstance().ShowUIForms("PlayerInventory");
        UIManager.GetInstance().CloseUIForms("PlayerInventory");
    }
    //英雄系统Update函数的调用
    public override void Update()
    {
        EventCenter.Broadcast(EventDefine.UI_SetHeroInfo, heroInfo.HeroState);
    }
    //系统结束回调
    public override void Release()
    {
        //TODO 玩家数据存入GamingData
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
        SetPlayerInfo2Inven();
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
        SetPlayerInfo2Inven();
    }
    //玩家受伤并判断死亡
    public void GetDamaged(float _damage)
    {
        heroInfo.HeroState.hp -= _damage;
        SetPlayerInfo2Inven();
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
            SetPlayerInfo2Inven();
            return true;
        }
        return false;
    }
    //获得经验
    public void GetExp(int _exp)
    {
        Debug.Log(heroInfo.HeroState.level);
        if (heroInfo.HeroState.cueeExp + _exp > heroInfo.HeroState.levelUpExp)
        {
            heroInfo.HeroState.level++;
            _exp -= (heroInfo.HeroState.levelUpExp - heroInfo.HeroState.cueeExp);
            heroInfo.HeroState.levelUpExp = heroInfo.HeroState.level * 100;
            heroInfo.HeroState.maxHp = heroInfo.HeroState.level * 100;
            heroInfo.HeroState.maxMp = heroInfo.HeroState.level * 100;
            heroInfo.HeroState.cueeExp = 0;
            GetExp(_exp);
        }
        else
        {
            heroInfo.HeroState.cueeExp = _exp;
        }
        SetPlayerInfo2Inven();
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
        inGameSystem.SynopsisSystem.taskSystem.SetProgress(inventory.GetInventoryInfo);
    }
    //通知背包批量删除
    public void DeleteItems()
    {
        inventory.SendItems(inGameSystem.SynopsisSystem.currentTask.td.taskNeed);
    }
    //批量获得物品
    public void GetItems()
    {
        SetTaskReward(inGameSystem.SynopsisSystem.currentTask.qr);
    }
    //显示背包物品
    public void SetInventoryUI()
    {
        EventCenter.Broadcast(EventDefine.InitBag, inventory.GetInventoryInfo);
    }
    //在背包显示玩家装备
    public void SetEquipInfo2Inven()
    {
        equips.GetEquipInfo();
    }
    //在背包显示玩家属性
    public void SetPlayerInfo2Inven()
    {
        EventCenter.Broadcast(EventDefine.UI_SetPlayerInfo2Inven, heroInfo.HeroState);
    }
    //获取装备信息
    public void GetEquips(Dictionary<int, int> _equips)
    {
        equips.Init(_equips);
        SetPlayerInfo2Inven();
    }

}