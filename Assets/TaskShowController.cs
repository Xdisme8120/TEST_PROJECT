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
    }

}