#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):JsonReader.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):JsonReader
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
public class JsonReader
{
    #region 方法

    //读取json
    public static JsonData ReadJson(string path)
    {
        string json = File.ReadAllText(path);
        return JsonMapper.ToObject(json);
    }

    #endregion
}