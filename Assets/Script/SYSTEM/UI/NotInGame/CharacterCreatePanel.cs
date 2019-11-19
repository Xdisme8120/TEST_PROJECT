

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using System;
using UnityEngine.UI;


public class CharacterCreatePanel : BaseUIForm
{

    //类型
    string heroType = "swordman";
    //昵称

    string nikeNaem;
    //
    InputField Ipt_name;

    Image selected_Button_05;
    Image selected_Button_06;


    private void Awake()
    {
        selected_Button_05 = GameObject.Find("Selected_Button_05").GetComponent<Image>();
        selected_Button_06 = GameObject.Find("Selected_Button_06").GetComponent<Image>();
        Ipt_name = GameObject.Find("InputField(CharacterName)").GetComponent<InputField>();
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
        RigisterButtonObjectEvent("Button_02", p => Button_Underdevelopment());
        RigisterButtonObjectEvent("Button_03", p => Button_Underdevelopment());
        RigisterButtonObjectEvent("Button_04", p => Button_Underdevelopment());
        RigisterButtonObjectEvent("Button_05", p => Button_05());
        RigisterButtonObjectEvent("Button_06", p => Button_06());
        RigisterButtonObjectEvent("Button(Back)", p => Button_Back());
        RigisterButtonObjectEvent("Button(Create)", p => Button_Create());
        selected_Button_06.gameObject.SetActive(false);

        Debug.Log("heroType1:" + heroType);
        Debug.Log("nikeNaem1:" + nikeNaem);
    }

    private void Button_Create()
    {
        Debug.Log("nikeNaem2:" + nikeNaem);

        Debug.Log("heroType2:" + heroType);
        Debug.Log(GamingData.heroList[0]);
        Debug.Log(GamingData.heroList[1]);
        nikeNaem = Ipt_name.text;
        if (nikeNaem == null || nikeNaem == "")
        {
            UIManager.GetInstance().ShowMessage("英雄名称不能为空");
            return;
        }
        else if (heroType == "swordman" && GamingData.heroList[0] != "" && GamingData.heroList[0] != "-1")
        {
            UIManager.GetInstance().ShowMessage("男性角色已存在 请不要重复创建");
            return;
        }
        else if (heroType == "tank" && GamingData.heroList[1] != "" && GamingData.heroList[1] != "-1")
        {
            UIManager.GetInstance().ShowMessage("女性角色已存在 请不要重复创建");
            return;
        }
        if (heroType== "swordman")
        {
            nikeNaem = nikeNaem + 1;
        }
        if (heroType == "tank")
        {
            nikeNaem = nikeNaem + 0;
        }

        EventCenter.Broadcast(EventDefine.CreateHero, heroType, nikeNaem);
        EventCenter.Broadcast(EventDefine.GetHeroListInfo,GamingData.username);
    }

    private void Button_Back()
    {
        heroType = "swordman";
        nikeNaem = null;
        UIManager.GetInstance().ShowUIForms("SelectHero");
        UIManager.GetInstance().CloseUIForms("CharacterCreate");
        EventCenter.Broadcast(EventDefine.UpdateHeroList);
    }

    /// <summary>
    /// 设置性别女(设置角色tank)
    /// </summary>
    private void Button_06()
    {
        selected_Button_05.gameObject.SetActive(false);
        selected_Button_06.gameObject.SetActive(true);
        heroType = "tank";
    }
    /// <summary>
    /// 设置性别男(设置角色swordman)
    /// </summary>
    private void Button_05()
    {
        selected_Button_05.gameObject.SetActive(true);
        selected_Button_06.gameObject.SetActive(false);
        heroType = "swordman";
    }

    //未开发
    private void Button_Underdevelopment()
    {
        UIManager.GetInstance().ShowMessage("该角色未开发");

    }
}

