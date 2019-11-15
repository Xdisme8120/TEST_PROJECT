using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using System;

public class PlayerInventory : BaseUIForm
{
    
    private void Awake()
    {
        CurrentUIType.UIForms_Type = UIFormType.PopUp;
        RigisterButtonObjectEvent("Btn_CloseInv", p => ClosePlayerInventory());
    }

    private void ClosePlayerInventory()
    {
        UIManager.GetInstance().CloseUIForms("PlayerInventory");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
