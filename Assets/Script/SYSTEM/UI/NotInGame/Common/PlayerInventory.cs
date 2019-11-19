using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using System;

public class PlayerInventory : BaseUIForm
{
    private Dictionary<int, BagGrid> bagInfo;
    private BagGrid[] rectTransforms;
    private void Awake()
    {
        bagInfo = new Dictionary<int, BagGrid>();
        //UIManager.GetInstance().CloseUIForms("PlayerInventory");
        CurrentUIType.UIForms_Type = UIFormType.Normal;
        RigisterButtonObjectEvent("Btn_CloseInv", p => ClosePlayerInventory());

        rectTransforms = UnityHelper.FindTheChildNode(gameObject, "SlotsGrid").GetComponentsInChildren<BagGrid>();
        for (int i = 1; i <= rectTransforms.Length; i++)
        {
            bagInfo.Add(i, rectTransforms[i - 1]);

        }
        //注册给格子赋值方法
        EventCenter.AddListener<int, Item, int>(EventDefine.UI_SetBagInfo,
            UI_SetBagInfo);
        //注册初始化背包方法
        EventCenter.AddListener<Dictionary<int, GridInfo>>(EventDefine.InitBag,
            InitBag);
    }

    private void ClosePlayerInventory()
    {
        UIManager.GetInstance().CloseUIForms("PlayerInventory");
        SendBagInfo();
    }

    // Use this for initialization

    //给格子赋值
    public void UI_SetBagInfo(int bagID, Item goodID, int goodCount)
    {
        bagInfo[bagID].SetGoodInfo(goodID, goodCount);
    }
    //开始游戏初始化背包
    public void InitBag(Dictionary<int, GridInfo> bagInfo)
    {
        for (int i = 1; i <= 8; i++)
        {
            UI_SetBagInfo(i, bagInfo[i].item, bagInfo[i].itemCount);
        }
    }
    //开始游戏初始化背包
    public void SendBagInfo()
    {
        //更新Bag信息
        rectTransforms = UnityHelper.FindTheChildNode(gameObject, "SlotsGrid").GetComponentsInChildren<BagGrid>();
        Dictionary<int, GridInfo> message = new Dictionary<int, GridInfo>();
        for (int i = 0; i < rectTransforms.Length ; i++)
        {
            message.Add(i+1,new GridInfo(i+1,rectTransforms[i].GetItem,rectTransforms[i].GetCount));
        }
        //广播更新bagInfo
        EventCenter.Broadcast(EventDefine.UI_SendBagInfo, message);
    }

}
