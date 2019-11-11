using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Json;
using LitJson;
public class CSInertactiveW : MonoBehaviour
{
    private void Awake()
    {
        //将功能注册进事件系统
        //注册 登陆 修改密码 创建英雄 获取英雄信息
        EventCenter.AddListener<string>(EventDefine.GetHeroInfo, GetHeroInfo);
        EventCenter.AddListener<string, string>(EventDefine.Login, Login);
        EventCenter.AddListener<string, string>(EventDefine.CreateHero, CreateHero);
        EventCenter.AddListener<string, string, string>(EventDefine.Register, Register);
        EventCenter.AddListener<string, string, string>(EventDefine.ChangePassword, ChangePassword);

    }
    //TODO 
    //1.获取英雄信息,并将英雄信息存入GamingData
    bool isLogined = false;
    //当前用户名
    string currUsername;
    //登陆状态索引器
    public bool IsLogined
    {
        get { return isLogined; }
    }
    //当前用户名索引器
    public string CurrUsername
    {
        get { return currUsername; }
    }
    //登陆/////////////////////////////////////////////////
    public void Login(string _username, string _password)
    {
        if (_username.Length <= 0 || _password.Length <= 0)
        {
            //TODO提示信息 信息不完整
            return;
        }
        StartCoroutine(ELogin(_username, _password));
    }
    IEnumerator ELogin(string _username, string _password)
    {
        WWWForm form = new WWWForm();
        form.AddField("a", "login");
        form.AddField("username", _username);
        form.AddField("password", _password);
        WWW www = new WWW("49.232.47.199/server/index.php", form);
        while (!www.isDone)
        {
            Debug.Log("wait");
        }
        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.text);
        }
    }
    /////////////////////////////////////////////////////

    //注册///////////////////////////////////////////////
    public void Register(string _username, string _password, string _checkPassword)
    {
        if (_password != _checkPassword)
        {
            //TODO 提示信息两次密码不一样
            return;
        }
        StartCoroutine(ERigister(_username, _password));
    }
    IEnumerator ERigister(string _username, string _password)
    {
        WWWForm form = new WWWForm();
        form.AddField("a", "register");
        form.AddField("username", _username);
        form.AddField("password", _password);
        WWW www = new WWW("49.232.47.199/server/index.php", form);
        while (!www.isDone)
        {
            Debug.Log("wait");
        }
        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.text);
        }
    }
    /////////////////////////////////////////////////////

    //修改密码////////////////////////////////////////////

    void ChangePassword(string _username, string _oldPassword, string _newPassword)
    {
        StartCoroutine(EChangePassword(_username, _oldPassword, _newPassword));
    }
    //TODO如果旧密码错误则提示无法修改
    IEnumerator EChangePassword(string _username, string _oldPassword, string _newPassword)
    {
        WWWForm form = new WWWForm();
        form.AddField("a", "updateUser");
        form.AddField("username", _username);
        form.AddField("oldpassword", _oldPassword);
        form.AddField("newpassword", _newPassword);
        WWW www = new WWW("49.232.47.199/server/index.php", form);
        while (!www.isDone)
        {
            Debug.Log("wait");
        }
        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.text);
        }
    }
    /////////////////////////////////////////////////////

    //创建新英雄//////////////////////////////////////////
    public void CreateHero(string _heroType, string _nickName)
    {
        StartCoroutine(ECreaterHero(_heroType, _nickName));
    }

    IEnumerator ECreaterHero(string _heroType, string _nickName)
    {
        WWWForm form = new WWWForm();
        form.AddField("a", "createHero");
        form.AddField("username", currUsername);
        form.AddField("nikename", CreateHeroJson(_heroType, _nickName));
        WWW www = new WWW("49.232.47.199/server/index.php", form);
        while (!www.isDone)
        {
            Debug.Log("wait");
        }
        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.text);
        }
    }
    //获取英雄信息////////////////////////////////////////
    public void GetHeroInfo(string _nickName)
    {
        StartCoroutine(_nickName);
    }
    IEnumerator EGetHeroInfo(string _nickName)
    {
        //TODO 从服务器抓取对应英雄信息并用DealWithHeroInfo()方法进行处理
        yield return null;
    }

    public Dictionary<string, JsonData> DealWithHeroInfo(string _jsonText)
    {
        //TODO 处理
        return new Dictionary<string, JsonData>();
    }
    /////////////////////////////////////////////////////

    /////////////////////////////////////////////////////

    //JSON
    // 创建英雄是存储的JSon
    public string CreateHeroJson(string _heroType, string _nickName)
    {

        JsonObject json = new JsonObject();
        json.Add(_heroType, _nickName);
        json.Add("heroName", _nickName);
        return json.ToString();
    }

    // 存储英雄信息是生成的JSon
    public string SaveHeroJson()
    {
        JsonObject json = new JsonObject();
        json.Add("level", 21);
        json.Add("CurrExp", 41);
        json.Add("Hp", 1);
        json.Add("Mp", 0);
        for (int i = 1; i <= 8; i++)
        {
            json.Add("bagItem" + i, i + 10);
        }
        for (int i = 1; i <= 6; i++)
        {
            json.Add("weapon" + i, i + 10);
        }

        return json.ToString();
    }


}


