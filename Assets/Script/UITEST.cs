using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
public class UITEST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //UIManager.GetInstance().ShowUIForms("Main");
        //登陆
        UIManager.GetInstance().ShowUIForms("Login");
        //找回密码
        //UIManager.GetInstance().ShowUIForms("Retrieve");
        //注册
        //UIManager.GetInstance().ShowUIForms("Register");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
