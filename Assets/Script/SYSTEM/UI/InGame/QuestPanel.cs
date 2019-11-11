using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
public class QuestPanel : BaseUIForm
{
    private void Awake()
    {
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Close()
    {
        Debug.Log("Close");
        UIManager.GetInstance().CloseUIForms("Quest");
    }
}
