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
//英雄系统_存储实时英雄信息
public class HeroSystem : IMainGameSystem
{
    //英雄信息
    HeroInfo heroInfo;
    //英雄是否处于可控状态
    bool isController;
    //是否处于死亡状态
    bool isDeath;

    public HeroSystem(InGameSystem _inGameSystem) : base(_inGameSystem)
    {
    }

    //设置英雄信息
    public void SetHeroInfo(GamingData _data)
    {}


    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }

    public override void Release()
    {
        throw new System.NotImplementedException();
    }
}