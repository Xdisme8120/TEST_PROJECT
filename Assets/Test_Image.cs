#region 模块信息
// **********************************************************************
// Copyright (C) 2019 QIANFENG EDUCATION
//
// 文件名(File Name):Test_Image.cs
// 公司(Company):#COMPANY#
// 作者(Author):#AUTHOR#
// 版本号(Version):#VERSION#
// Unity版本	(Unity Version):#UNITYVERSION#
// 创建时间(CreateTime):#DATE#
// 修改者列表(modifier):无
// 模块描述(Module description):Test_Image
// **********************************************************************
#endregion
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Test_Image : MonoBehaviour {
    Image image;
    private void Awake() {
        image = GetComponent<Image>();
        EventCenter.AddListener<int>(EventDefine.Test_GetImage,SetItem);
    }

    public void SetItem(int _itemID)
    {
        image.sprite = Resources.Load<Sprite>("Item"+_itemID);
    }
}