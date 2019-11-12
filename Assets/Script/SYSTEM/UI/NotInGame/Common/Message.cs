using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;


public class Message : BaseUIForm{

    private void Awake()
    {
        CurrentUIType.UIForms_Type = UIFormType.PopUp;
        RigisterButtonObjectEvent("Btn_Close", p => CloseMessage());

    }

    //关闭弹窗
    private void CloseMessage()
    {
        UIManager.GetInstance().CloseUIForms("Message");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
