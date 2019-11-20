#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):TestQuit.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):TestQuit
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;

public class TestQuit : MonoBehaviour {
    public void Quit()
    {
        GameSystem.Instance.SetNewSceneState(new LoginSceneState(GameSystem.Instance.sceneStateController),0);
    }
}