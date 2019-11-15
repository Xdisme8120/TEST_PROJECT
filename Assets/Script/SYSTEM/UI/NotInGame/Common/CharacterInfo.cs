using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using System;

public class CharacterInfo : BaseUIForm
{
    private void Awake()
    {

        CurrentUIType.UIForms_Type = UIFormType.Normal;
        RigisterButtonObjectEvent("Btn_CloseChar", p => CloseCharInfo());

    }

    private void CloseCharInfo()
    {
        UIManager.GetInstance().CloseUIForms("CharacterInfo");
        //throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
