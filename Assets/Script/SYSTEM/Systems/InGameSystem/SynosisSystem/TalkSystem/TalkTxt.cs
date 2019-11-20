#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):TalkTxt.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):TalkTxt
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
public class TalkTxt
{
    #region 属性和字段

    //聊天系统
    //SaveTalkTxt saveTalkTxt = new SaveTalkTxt();
    ShowTxt[] showTxts;
    //文件路径
    private string path;

    #endregion

    #region 方法   

    /// <summary>
    /// 根据npc当前的状态获取到对应的文本
    /// </summary>
    /// <param name="npcState"> npc状态</param>
    /// <param name="ID">npcID</param>
    /// <param name="talkSystem">对话系统示例</param>
    public void GetTxt(NpcState npcState, int ID,TalkSystem talkSystem)
    {
        talkSystem.InitTalk();
        path = Application.streamingAssetsPath + "/TalkTxt/" + ID + "/" + npcState + "/1.json";
        showTxts = JsonMapper.ToObject<ShowTxt[]>(JsonMapper.ToJson(JsonReader.ReadJson(path)["ShowTxt"]));
        for (int i = 0; i < showTxts.Length; i++)
        {
            SaveTalkTxt.GetInstance().dic.Add(i + 1, showTxts[i]);
        }
    }

    #endregion
}