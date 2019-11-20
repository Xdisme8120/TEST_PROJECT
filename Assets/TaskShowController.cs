#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):TaskShowController.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):TaskShowController
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using SUIFW;

public class TaskShowController : MonoBehaviour
{
    public static int index = 0;
    public static int inventoryIndex = 0;
    bool isFristOpenInventory;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            index++;
            if (index % 2 == 0)
            {
                //Debug.Log("关闭面板");
                UIManager.GetInstance().CloseUIForms("MainTaskShow");
                SynopsisSystem.isFirst = true;
            }
            else
            {
                UIManager.GetInstance().ShowUIForms("MainTaskShow");
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(!isFristOpenInventory)
            {
                EventCenter.Broadcast(EventDefine.InitEquipC);
                isFristOpenInventory = true;
            }
            inventoryIndex++;
            if (inventoryIndex % 2 == 0)
            {
                UIManager.GetInstance().CloseUIForms("PlayerInventory");
            }
            else
            {
                UIManager.GetInstance().ShowUIForms("PlayerInventory");
                //发送背包信息给UI的一系列
                EventCenter.Broadcast(EventDefine.UI_SetInventoryC);
                EventCenter.Broadcast(EventDefine.UI_SetPlayerInfo2InvenC);
            }
        }
    }

}