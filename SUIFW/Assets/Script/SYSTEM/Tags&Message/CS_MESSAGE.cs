using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MESSAGE
{
    //登陆时服务器的信息反馈
    public static readonly string  MSG_Login_0 = "登陆成功";
    public static readonly string  MSG_Login_1 = "用户名或密码错误";

    //注册时服务器服务器信息反馈
    public static readonly string  MSG_Rigester_0 = "修改成功";
    public static readonly string  MSG_Rigester_1 = "修改失败";

    //修改密码时服务器动信息反馈
    public static readonly string  MSG_ChangePassword_0 = "修改成功";
    public static readonly string  MSG_ChangePassword_1 = "修改失败";
    
    //创建英雄时的服务器信息反馈
    public static readonly string  MSG_CreateHero_0 = "创建成功";
    public static readonly string  MSG_CreateHero_1 = "创建失败,已有当前职业英雄";
    public static readonly string  MSG_CreateHero_2 = "创建失败,昵称已存在"; 
}
