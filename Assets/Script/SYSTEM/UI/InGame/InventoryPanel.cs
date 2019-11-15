using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;


public class InventoryPanel : BaseUIForm
{
    
    private void Awake()
    {
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;

    }
    // Update is called once per frame
    public void Close()
    {
        Debug.Log("Close");
        UIManager.GetInstance().CloseUIForms("Inventory");
    }

}