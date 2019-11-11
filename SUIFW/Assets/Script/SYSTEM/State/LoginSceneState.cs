using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
public class LoginSceneState : SceneState
{
    public LoginSceneState(SceneStateController controller) : base(controller)
    {}

    public override void StateBegin()
    { 
        //如果处于未登录状态，则进入登陆界面
        if(GameSystem.Instance.GetLoginState())
        {
            UIManager.GetInstance().ShowUIForms("SelectHero");
        }
        //否则显示英雄选择界面
        else
        {
            UIManager.GetInstance().ShowUIForms("Login");
        }
    }
    public override void StateUpdate()
    { }
    public override void StateEnd()
    { 
        //英雄选择
    }
}
