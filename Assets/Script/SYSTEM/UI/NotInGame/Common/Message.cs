using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using UnityEngine.UI;

public class Message : BaseUIForm{

    Text message;
    private void Awake()
    {
        CurrentUIType.UIForms_Type = UIFormType.PopUp;
        RigisterButtonObjectEvent("Btn_Close", p => CloseMessage());
        message = UnityHelper.FindTheChildNode(gameObject,"Content").GetComponent<Text>();
        EventCenter.AddListener<string>(EventDefine.Message,ShowMessage);
    }

    //关闭弹窗
    private void CloseMessage()
    {
        UIManager.GetInstance().CloseUIForms("Message");
    }
    //显示信息
    public void ShowMessage(string _message)
    {
        message.text = _message;
    }

}
