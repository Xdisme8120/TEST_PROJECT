using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;

public class PlayerInfo : BaseUIForm
{
    private void Awake()
    {
        UIManager.GetInstance().CloseUIForms("PlayerInfo");
        CurrentUIType.UIForms_Type = UIFormType.Fixed;
        //RigisterButtonObjectEvent("Btn_Close", p => CloseMessage());
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
