#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):TaskShow.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):TaskShow
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TaskShow : MonoBehaviour {

    #region 属性和字段

    public Text nameTxt;
    public Text progress;
    List<string> list;
    ShowUI showUI;

    //*************测试
    Dictionary<string, int> _progress;
    Dictionary<string, int> _need;
    //*************

    #endregion

    #region 生命周期
    private void Awake()
    {
        list = new List<string>();
        nameTxt = transform.Find("name").GetComponent<Text>();
        progress = transform.Find("progress").GetComponent<Text>();
        showUI = new ShowUI();
        //******************测试
        _progress = new Dictionary<string, int>();
        _need = new Dictionary<string, int>();

        _progress.Add("刀剑", 1);
        _progress.Add("棍棒", 2);
        _need.Add("刀剑", 5);
        _need.Add("棍棒", 5);
        //*****************
        EventCenter.AddListener<TaskSystem>(EventDefine.MiniTaskShow, Show);
    }

    private void Start()
    {
        //**********************测试
        //InitTaskSPanel("武松打虎", _progress, _need);
        //**********************
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<TaskSystem>(EventDefine.MiniTaskShow,Show);
    }

    #endregion

    #region 方法

    /// <summary>
    /// 展示任务小面板
    /// </summary>
    /// <param name="taskSystem"></param>
    public void Show(TaskSystem taskSystem)
    {
        nameTxt.text = taskSystem.sy.currentTask.name;
        showUI.ShowNeeds(progress,taskSystem);
        //*****************************************测试
        //Debug.Log(taskSystem.sy.currentTask.tp.taskComplete["情报贩子"]);
    }

    /// <summary>
    ///游戏开始时初始化小任务面板
    /// </summary>
    public void InitTaskSPanel(string taskName, Dictionary<string, int> _progress,Dictionary<string,int> need)
    {
        int i = 0;
        if (_progress.Count != need.Count)
        {
            Debug.Log("传输数据有误");
            return;
        }
        nameTxt.text = taskName;
        //展示任务进度
        foreach (var item in _progress)
        {
            list.Add(item.Key+":"+ item.Value + "/");
        }
        //展示任务需求
        foreach (var item in need)
        {
            list[i] += item.Value;
            i++;
        }
        //合并字符串
        for (int j = 0; j < list.Count; j++)
        {
            if (j == list.Count - 1)
            {
                progress.text += list[j];
            }
            else
            {
                progress.text += list[j] + "\n";
            }
        }
    }

    #endregion
}