﻿#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):LoginPanel.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):LoginPanel
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SUIFW;
using System;
//登陆界面
public class RetrievePanel : BaseUIForm
{

    //登陆按钮
    Button btn_Login;
    //注册按钮
    Button btn_Register;
    //修改密码按钮
    Button btn_ChangePassword;
    //用户名输入
    InputField if_Username;
    //密码输入
    InputField if_Password;
    //TODO登陆界面的初始化
    private void Awake()
    {
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        RigisterButtonObjectEvent("Button(Change)", p => Change());
        RigisterButtonObjectEvent("Button(Return)", p => Return());
        //RigisterButtonObjectEvent("Button(Retrieve)", p => Retrieve());

    }
    //修改密码事件
    private void Change()
    {
        Debug.Log("忘记密码");
        //throw new NotImplementedException();
    }
    //返回登陆页面
    private void Return()
    {
        UIManager.GetInstance().CloseUIForms("Retrieve");
        Debug.Log("返回");
        //throw new NotImplementedException();
    }
}