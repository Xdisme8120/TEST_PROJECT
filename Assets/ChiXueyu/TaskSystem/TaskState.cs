#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):TaskState.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):TaskState
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;

public enum TaskState
{
    #region 枚举类型

    //接取后待刷新
    Refresh,
    //待接取
    PickUp,
    //任务中
    Tasking,
    //未提交
    UnSubmit,
    //已经完成
    Completed,

    #endregion
}