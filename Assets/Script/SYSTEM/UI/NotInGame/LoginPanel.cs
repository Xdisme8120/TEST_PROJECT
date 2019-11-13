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
public class LoginPanel : BaseUIForm
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
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
        RigisterButtonObjectEvent("Button(Login)", p => Login());
        RigisterButtonObjectEvent("Button(Register)", p => Register());
        RigisterButtonObjectEvent("Button(Retrieve)", p => Retrieve());
        if_Username = UnityHelper.FindTheChildNode(gameObject,"Input Field (Username)").GetComponent<InputField>();
        if_Password = UnityHelper.FindTheChildNode(gameObject,"Input Field (Password)").GetComponent<InputField>();

    }
    private void Start()
    {
    }
    //忘记密码页面跳转
    private void Retrieve()
    {
        UIManager.GetInstance().ShowUIForms("Retrieve");
        Debug.Log("忘记密码");
        //throw new NotImplementedException();
    }
    //注册页面跳转
    private void Register()
    {
        Debug.Log("注册");
        UIManager.GetInstance().ShowUIForms("Register");
        //throw new NotImplementedException();
    }
    //登陆事件
    private void Login()
    {
        //打开头像信息固定窗口
        UIManager.GetInstance().ShowUIForms("PlayerInfo");
        //打开人物信息固定窗口
        UIManager.GetInstance().ShowUIForms("CharacterInfo");
        //打开背包窗口
        UIManager.GetInstance().ShowUIForms("PlayerInventory");
        //关闭登陆界面
        UIManager.GetInstance().CloseUIForms("Login");
        Debug.Log("登陆");
        EventCenter.Broadcast(EventDefine.Login, if_Username.text, if_Password.text);
    }
}