#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):MoveTxt.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):MoveTxt
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;

/// <summary>
/// 测试场景中的移动脚本
/// </summary>
public class MoveTxt : MonoBehaviour
{
    #region 属性和字段
    public float ver;
    public float hor;
    public float speed = 0.1f;
    #endregion

    #region 生命周期
    private void Update()
    {
        ver = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");
        transform.position = transform.position + new Vector3(hor*speed, 0, ver*speed);
    }
    #endregion
}