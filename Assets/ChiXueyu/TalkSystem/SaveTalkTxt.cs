#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):SaveTalkTxt.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):SaveTalkTxt
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveTalkTxt
{
    #region 属性和字段

    //存储当前对话的数据结构
    static SaveTalkTxt instance;
    //字典存储
    public Dictionary<int, ShowTxt> dic = new Dictionary<int, ShowTxt>();

    #endregion

    #region 方法

    /// <summary>
    /// 构造函数
    /// </summary>
    private SaveTalkTxt()
    {
        dic = new Dictionary<int, ShowTxt>();
    }

    /// <summary>
    /// 单例的抓取
    /// </summary>
    /// <returns></returns>
    public static SaveTalkTxt GetInstance()
    {
        if (instance == null)
        {
            instance = new SaveTalkTxt();
            return instance;
        }
        return instance;
    }

    #endregion 
}