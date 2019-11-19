#region 模块信息
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
public class RegisterPanel : BaseUIForm
{
    //用户名输入
    InputField if_Username;
    //密码输入
    InputField if_Password;
    InputField if_Password_two;

    //TODO登陆界面的初始化
    private void Awake()
    {
        
        if_Username = UnityHelper.FindTheChildNode(gameObject, "InputField(Username)").GetComponent<InputField>();
        if_Password = UnityHelper.FindTheChildNode(gameObject, "InputField(Password)_1").GetComponent<InputField>();
        if_Password_two = UnityHelper.FindTheChildNode(gameObject, "InputField(Password)_2").GetComponent<InputField>();

        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        RigisterButtonObjectEvent("Button(Register)", p => Register());
        RigisterButtonObjectEvent("Button(Return)", p => Return());
    }

    //返回登陆页面
    private void Return()
    {
        UIManager.GetInstance().CloseUIForms("Register");
    }
    //注册事件
    private void Register()
    {
        EventCenter.Broadcast(EventDefine.Register, if_Username.text, if_Password.text, if_Password_two.text);
    }
}