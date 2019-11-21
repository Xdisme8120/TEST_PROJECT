using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using System;
using UnityEngine.UI;
public class PlayerInventory : BaseUIForm
{
    private Dictionary<int, BagGrid> bagInfo;
    private BagGrid[] bagArray;
    //装备
    private Dictionary<int, BagGrid> equipInfo;
    private BagGrid[] equipArray;
    Text Hp;
    Text Mp;
    Text Exp;
    Text Def;
    Text Attack;
    private void Awake()
    {
        //背包赋值
        bagInfo = new Dictionary<int, BagGrid>();
        bagArray = UnityHelper.FindTheChildNode(gameObject, "SlotsGrid").GetComponentsInChildren<BagGrid>();
        //装备栏信息赋值
        equipInfo = new Dictionary<int, BagGrid>();
        equipArray = UnityHelper.FindTheChildNode(gameObject, "CharacterContent").GetComponentsInChildren<BagGrid>();
        //BagGrid存入字典_Bag
        for (int i = 1; i <= bagArray.Length; i++)
        {
            bagInfo.Add(i, bagArray[i - 1]);
        }
        //BagGrid存入字典_Equip
        for (int i = 1; i <= equipArray.Length; i++)
        {
            equipInfo.Add(i, equipArray[i - 1]);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        Hp = UnityHelper.FindTheChildNode(gameObject, "Value_Hp").GetComponent<Text>();
        Mp = UnityHelper.FindTheChildNode(gameObject, "Value_Sp").GetComponent<Text>();
        Exp = UnityHelper.FindTheChildNode(gameObject, "Value_Exp").GetComponent<Text>();
        Attack = UnityHelper.FindTheChildNode(gameObject, "Value_Atk").GetComponent<Text>();
        Def = UnityHelper.FindTheChildNode(gameObject, "Value_Def").GetComponent<Text>();

        CurrentUIType.UIForms_Type = UIFormType.PopUp;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        //按钮关闭时间注册
        RigisterButtonObjectEvent("Btn_CloseInv", p => ClosePlayerInventory());


        //注册给装备赋值方法
        EventCenter.AddListener<int, Item>(EventDefine.UI_SetEquipInfo, UI_SetEquipInfo);
        //注册初始化装备背包方法
        EventCenter.AddListener<Dictionary<int, GridInfo>>(EventDefine.InitEquip, InitEquip);
        //注册给格子赋值方法
        EventCenter.AddListener<int, Item, int>(EventDefine.UI_SetBagInfo, UI_SetBagInfo);
        //注册初始化背包方法
        EventCenter.AddListener<Dictionary<int, GridInfo>>(EventDefine.InitBag, InitBag);
        //设置玩家数据方法注册
        EventCenter.AddListener<HeroState>(EventDefine.UI_SetPlayerInfo2Inven, UI_SetPlayerInfo);
        //发送装备事件注册
        EventCenter.AddListener(EventDefine.UI_SendEquipInfoV, SendEquipInfo);
    }
    //关闭背包面板
    private void ClosePlayerInventory()
    {
        TaskShowController.inventoryIndex++;
        SendBagInfo();
        UIManager.GetInstance().CloseUIForms("PlayerInventory");
    }

    //给玩家属性赋值
    public void UI_SetPlayerInfo(HeroState _heroState)
    {
        Hp.text = _heroState.hp.ToString() + "||" + _heroState.maxHp.ToString();
        Mp.text = _heroState.sp.ToString() + "||" + _heroState.maxMp.ToString();
        Attack.text = _heroState.atk.ToString();
        Def.text = _heroState.def.ToString();
        Exp.text = _heroState.cueeExp.ToString() + "||" + _heroState.levelUpExp.ToString();
    }
    //开始游戏初始化背包
    public void InitBag(Dictionary<int, GridInfo> _bagInfo)
    {
        bagArray = UnityHelper.FindTheChildNode(gameObject, "SlotsGrid").GetComponentsInChildren<BagGrid>();
        bagInfo.Clear();
        for (int i = 1; i <= bagArray.Length; i++)
        {
            bagInfo.Add(i,bagArray[i-1]);
        }
        for (int i = 1; i <= 8; i++)
        {
            Debug.Log(i + "__" + _bagInfo[i].item.Name);
            UI_SetBagInfo(i, _bagInfo[i].item, _bagInfo[i].itemCount);
        }
    }
    //给格子赋值
    public void UI_SetBagInfo(int bagID, Item goodID, int goodCount)
    {
        bagInfo[bagID].SetGoodInfo(goodID, goodCount);
    }


    //初始化装备栏
    public void InitEquip(Dictionary<int, GridInfo> equipInfo)
    {
        for (int i = 1; i <= 6; i++)
        {
            UI_SetEquipInfo(i, equipInfo[i].item);
        }
    }
    //给装备赋值
    public void UI_SetEquipInfo(int equipID, Item goodID)
    {
        equipInfo[equipID].SetEquipInfo(goodID);
    }

    //////////////////////////////////////////////
    /////发送UI装备信息给C层
    public void SendEquipInfo()
    {
        //更新Equip信息
        equipArray = UnityHelper.FindTheChildNode(gameObject, "CharacterContent").GetComponentsInChildren<BagGrid>();
        Dictionary<int, int> equipMessage = new Dictionary<int, int>();
        for (int i = 0; i < equipArray.Length; i++)
        {
            equipMessage.Add(i + 1, equipArray[i].GetItem.ID);
        }
        SendBagInfo();
        //广播更新EquipInfo
        EventCenter.Broadcast(EventDefine.UI_SendEquipInfo, equipMessage);
    }
    //发送UI背包信息给C层
    public void SendBagInfo()
    {
        //更新Bag信息
        bagArray = UnityHelper.FindTheChildNode(gameObject, "SlotsGrid").GetComponentsInChildren<BagGrid>();
        Dictionary<int, GridInfo> bagMessage = new Dictionary<int, GridInfo>();
        for (int i = 0; i < bagArray.Length; i++)
        {
            bagMessage.Add(i + 1, new GridInfo(i + 1, bagArray[i].GetItem, bagArray[i].GetCount));
        }
        //广播更新bagInfo
        EventCenter.Broadcast(EventDefine.UI_SendBagInfo, bagMessage);
    }
}
