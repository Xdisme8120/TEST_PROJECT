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
        for (int i = 0; i < rectTransforms.Length - 1; i++)
        {
            bagInfo.Add(i + 1, rectTransforms[i + 1]);
        }
        //注册给格子赋值方法
        EventCenter.AddListener<int,int,int>(EventDefine.UI_SetBagInfo,
            UI_SetBagInfo);
        //注册初始化背包方法
        EventCenter.AddListener<Dictionary<int, GridInfo>>(EventDefine.InitBag,
            InitBag);
    }

    private void ClosePlayerInventory()
    {
        UIManager.GetInstance().CloseUIForms("PlayerInventory");
    }

    // Use this for initialization
    private void Start()
    {
        UI_SetBagInfo(5, 102, 100);
    }
    //给格子赋值
    public void UI_SetBagInfo(int bagID, int goodID, int goodCount)
    {
        bagInfo[bagID-1].SetGoodInfo(goodID, goodCount);
    }
    //开始游戏初始化背包
    public void InitBag(Dictionary<int, GridInfo> bagInfo)
    {
        for (int i = 1; i <= 8; i++)
        {
            UI_SetBagInfo(i, bagInfo[i].item.ID, bagInfo[i].itemCount);
        }
    }
    //开始游戏初始化背包
    public void SendBagInfo()
    {
        //更新Bag信息
        rectTransforms = UnityHelper.FindTheChildNode(gameObject, "SlotsGrid").GetComponentsInChildren<BagGrid>();
        for (int i = 0; i < rectTransforms.Length - 1; i++)
        {
            bagInfo.Add(i + 1, rectTransforms[i + 1]);
        }
        //广播更新bagInfo
        EventCenter.Broadcast(EventDefine.SetBagInfo, bagInfo);
    }

}
