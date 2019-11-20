#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):TalkCallBack.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):TalkCallBack
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TalkCallBack : MonoBehaviour
{
    #region 属性和字段

    //UI界面底图
    public static GameObject talkBg;
    //UI界面名字文本
    private Text nameText;
    //UI界面对话文本
    private Text talk;
    //是否是第一次显示，第一次显示需抓取文本
    public static bool isFirst = true;
    //文本抓取
    TalkTxt talkTxt;
    //显示文本 

    #endregion

    #region 生命周期

    private void Awake()
    {
        EventCenter.AddListener<SynopsisSystem, TalkSystem>(EventDefine.ShowTalkText, TalkShow);
        talkTxt = new TalkTxt();       
        talkBg = transform.Find("Image").gameObject;
        nameText = transform.Find("Image/name").GetComponent<Text>();
        talk = transform.Find("Image/txt").GetComponent<Text>();     
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<SynopsisSystem,TalkSystem>(EventDefine.ShowTalkText, TalkShow);
    }

    #endregion

    #region 方法

    /// <summary>
    /// 抓取文本并设置
    /// </summary>
    private void TalkShow(SynopsisSystem _sy,TalkSystem talkSystem)
    {
        if (isFirst)
        {
            //设置剧情系统正在对话中
            _sy.SetTalkState(TalkState.Talking);
            talkTxt.GetTxt(_sy.currentNpc.NpcState, _sy.currentNpc.NpcID,talkSystem);
            isFirst = false;
        }    
        ShowTxt showTxt =  talkSystem.ShowTxts();
        //********************************
        if(showTxt != null)
        {
            Talk(showTxt.name, showTxt.txt);
        }
    }

    /// <summary>
    /// 将获取到的文本显示在UI界面上
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_talk"></param>
    private void Talk(string _name, string _talk)
    {
        if (!talkBg.activeSelf)
        {
            talkBg.SetActive(true);
        }
        nameText.text = _name;
        talk.text = _talk;
    }

    #endregion
}