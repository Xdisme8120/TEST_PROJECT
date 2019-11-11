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

public class GamingDataController :BaseSystemController
{
    //数据实体
    GamingData data;
    public  GamingDataController(GameSystem _system):base(_system)
    {
        //实例化数据实体
        data = new GamingData();
    }
    //初始化数据
    public void InitData()
    {}
    //存储数据
    public void SaveData()
    {}
    //获取数据
    public void GetData()
    {}

}