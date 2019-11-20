#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):TaskDemand.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):TaskDemand
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskDemand
{
    #region 属性和字段

    public Dictionary<string, int> taskNeed;

    #endregion

    #region 构造方法

    public TaskDemand()
    {
        taskNeed = new Dictionary<string, int>();
    }

    #endregion
}