#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):InGameSystem.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):InGameSystem
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;

public class InGameSystem
{
    public HeroSystem HeroSystem
    {
        get { return heroSystem; }
    }
    public SynopsisSystem SynopsisSystem
    {
        get { return synopsisSystem; }
    }
    //


    public InGameSystem()
    {
        //初始化系统
        heroSystem = new HeroSystem(this);
        synopsisSystem = new SynopsisSystem(this);
        Init(heroSystem);
    }
    //英雄系统
    HeroSystem heroSystem;
    //剧情系统
    SynopsisSystem synopsisSystem;

  
    //初始化游戏内部系统
    public void Init(IMainGameSystem _system)
    {
        _system.Init();
    }
    public void Update(IMainGameSystem _system)
    {
        _system.Update();
    }
    public void Release(IMainGameSystem _system)
    {
        _system.Release();
    }

}