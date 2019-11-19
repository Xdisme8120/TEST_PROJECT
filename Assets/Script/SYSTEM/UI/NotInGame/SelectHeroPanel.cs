#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):SelectHeroPanel.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):SelectHeroPanel
// **********************************************************************
#endregion
using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using SUIFW;
using System;

public class SelectHeroPanel : BaseUIForm
{
    Transform selectCharacter;
    Image image;

    private void Awake()
    {
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
        RigisterButtonObjectEvent("Button (Create)", p => CreateHero());
        RigisterButtonObjectEvent("Button (Play)", p => PlayGame());
        selectCharacter = UnityHelper.FindTheChildNode(gameObject, "Characters").GetComponent<Transform>();
        image = UnityHelper.FindTheChildNode(gameObject, "ImageHeroSelect").GetComponent<Image>();
        UpdateHeroList();
        image.gameObject.SetActive(false);
        EventCenter.AddListener(EventDefine.UpdateHeroList, UpdateHeroList);
    }


    private void PlayGame()
    {
        //进入游戏主页面
        //关闭当前页面
        UIManager.GetInstance().CloseUIForms("SelectHero");
        Debug.Log(GamingData.nickname);
        Debug.Log("跳转场景");
    }

    private void CreateHero()
    {
        //创建英雄
        //打开创建英雄页面
        UIManager.GetInstance().ShowUIForms("CharacterCreate");
        //关闭选择英雄页面
        UIManager.GetInstance().CloseUIForms("SelectHero");
    }

    public void UpdateHeroList()
    {
        for (int i = 0; i < 2; i++)
        {
            if (GamingData.heroList[i] != "-1" && GamingData.heroList[i] != "")
            {
                if (UnityHelper.FindTheChildNode(gameObject, GamingData.heroList[i]) == null)
                {
                    GameObject objTemp = Instantiate(Resources.Load<GameObject>("Character(List)"), selectCharacter);
                    objTemp.name = GamingData.heroList[i];
                    objTemp.transform.GetChild(0).GetComponent<Text>().text = GamingData.heroList[i];
                    objTemp.transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/102");
                    if (GamingData.heroLT[GamingData.heroList[i]]==0)
                    {
                        objTemp.transform.GetChild(4).GetComponent<Text>().text = "女性角色";
                    }
                    if (GamingData.heroLT[GamingData.heroList[i]] == 1)
                    {
                        objTemp.transform.GetChild(4).GetComponent<Text>().text = "男性角色";
                    }

                    RigisterButtonObjectEvent(GamingData.heroList[i], p =>
                    {
                        image.transform.SetParent(objTemp.transform);
                        image.gameObject.SetActive(true);
                        image.rectTransform.localPosition = new Vector3(400, -50, 0);
                        GameSystem.Instance.gamingDataController.SetNickname(objTemp.name);
                    }
                    );
                }
            }
        }
    }
}