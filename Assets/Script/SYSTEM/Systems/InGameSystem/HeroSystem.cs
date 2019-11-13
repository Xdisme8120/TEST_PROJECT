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
    {}

    //设置英雄信息
    public void SetHeroInfo(GamingData _data)
    {}

    //初始化英雄系统
    public override void Init()
    {
        heroInfo = new HeroInfo(this);
        inventory = new Inventory(this);
        equips = new Equips(this);
        heroInfo.InitHeroInfo(GamingData.INSTANCE().HeroState);
        inventory.Init(GamingData.INSTANCE().InvenrotyInfo);
        equips.Init(GamingData.INSTANCE().EquipsInfo);
    }
    //英雄系统Update函数的调用
    public override void Update()
    {
        //
    }
    //系统结束回调
    public override void Release()
    {
        throw new System.NotImplementedException();
    }
}