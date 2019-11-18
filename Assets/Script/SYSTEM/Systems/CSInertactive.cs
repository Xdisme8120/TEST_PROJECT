using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
using System.Json;
using LitJson;
using SUIFW;
enum CSType
{
    Login,
    Register,
    CreateHero,
    GetHeroInfo,
    ChangePassword
}
public class CSInertactive : MonoBehaviour
{

    //主系统引用
    GameSystem system;
    //反馈信息栏
    Type messageInfos;
    private void Awake()
    {
        messageInfos = typeof(CS_MESSAGE);
        //获取主系统引用
        system = GameSystem.Instance;
        //将功能注册进事件系统
        //注册 登陆 修改密码 创建英雄 获取英雄信息
        //Debug.Log("事件注册");
        EventCenter.AddListener<string>(EventDefine.GetHeroInfo, GetHeroInfo);
        EventCenter.AddListener<string, string>(EventDefine.Login, Login);
        EventCenter.AddListener<string, string>(EventDefine.CreateHero, CreateHero);
        EventCenter.AddListener<string, string, string>(EventDefine.Register, Register);
        EventCenter.AddListener<string, string, string>(EventDefine.ChangePassword, ChangePassword);
        EventCenter.AddListener<HeroState, Dictionary<int, GridInfo>, Dictionary<int, int>>(EventDefine.SaveHeroInfo,SaveHeroInfo);

    }
    private void Start()
    {
        // EventCenter.Broadcast(EventDefine.GetHeroInfo,"Test");
    }
    //TODO 
    //1.获取英雄信息,并将英雄信息存入GamingData
    bool isLogined = false;
    //当前用户名
    string currUsername;
    //当前英雄昵称
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
            int code = GetErrorCode(www.text);
            if (code != 0)
                ShowMessageByCode("Login", code);
            else
            {
                GameSystem.Instance.gamingDataController.SetUsername(_username);
            }
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
            //Debug.Log("wait");
        }
        yield return www;
        if (www.error != null)
        {
            //Debug.Log(www.error);
        }
        else
        {
            //Debug.Log(www.text);
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
            //Debug.Log("wait");
        }
        yield return www;
        if (www.error != null)
        {
            //Debug.Log(www.error);
        }
        else
        {
            //Debug.Log(www.text);
        }
    }
    //获取英雄信息//////////////////////////////(//////////
    public void GetHeroInfo(string _nickName)
    {
        StartCoroutine(EGetHeroInfo(_nickName));
    }
    IEnumerator EGetHeroInfo(string _nickName)
    {
        WWWForm form = new WWWForm();
        form.AddField("a", "getAllHero");
        form.AddField("heroName", "bitch");
        WWW www = new WWW("49.232.47.199/server/index.php", form);
        while (!www.isDone)
        {
            //            Debug.Log("wait");
        }
        yield return www;
        if (www.error != null)
        {
            //Debug.Log(www.error);
        }
        else
        {
            //Debug.Log(www.text);
            //将收到的英雄信息传给数据处理系统
            system.gamingDataController.InitData(JsonMapper.ToObject(www.text));
            GameSystem.Instance.gamingDataController.SetNickname(_nickName);
        }
        yield return null;
    }
    //存储英雄信息到服务器////////////////////////////////////////////
    public void SaveHeroInfo(HeroState _stateData, Dictionary<int, GridInfo> _invenData, Dictionary<int, int> _equipsData)
    {
        string json = SaveHeroJson( _stateData,_invenData,_equipsData);
        Debug.Log("存储的Json"+json);
        for (int i = 0; i < 3; i++)
        {
            switch (i)
            {
                case 0:
                    StartCoroutine(POST("updateBagInfo", "bitch", json));
                    break;
                case 1:
                    StartCoroutine(POST("updateCharInfo", "bitch", json));
                    break;
                case 2:
                    StartCoroutine(POST("updateItemInfo", "bitch", json));
                    break;
            }
        }
    }
    //英雄信息存储协程
     IEnumerator POST(string _action, string _heroname, string _json)
    {
        WWWForm form = new WWWForm();
        form.AddField("a", _action);
        form.AddField("nikename", _heroname);
        form.AddField("jsondata", _json);
        WWW www = new WWW("49.232.47.199/server/index.php", form);
        while (!www.isDone)
        {
            //Debug.Log("wait");
        }
        yield return www;
        if (www.error != null)
        {
            //Debug.Log("ERROR" + Time.time);
        }
        else
        {
            //Debug.Log(www.text);
        }
    }
    
    /// ////////////////////////////////////////////////////////////
    //JSON
    // 创建英雄是存储的JSon
    public string CreateHeroJson(string _heroType, string _nickName)
    {

        JsonObject json = new JsonObject();
        json.Add(_heroType, _nickName);
        json.Add("heroName", _nickName);
        return json.ToString();
    }

    // 存储英雄信息时生成的JSon
    public string SaveHeroJson(HeroState _stateData, Dictionary<int, GridInfo> _invenData, Dictionary<int, int> _equipsData)
    {
        Dictionary<int, string> tempInvenInfo = new Dictionary<int, string>();
        Dictionary<int, int> tempEquipInfo = new Dictionary<int, int>();
        JsonObject json = new JsonObject();
        json.Add("level", _stateData.level);
        json.Add("CurrExp", _stateData.cueeExp);
        json.Add("Hp", (int)_stateData.hp);
        json.Add("Mp", (int)_stateData.sp);
        int index = 1;
        foreach (var item in _invenData)
        {
            string tempItemInfo;
            if (item.Value.GetItemID() != -1)
            {
                tempItemInfo = item.Value.item.ID + "|" + item.Value.itemCount;
            }
            else
            {
                tempItemInfo = "-1";
            }
            json.Add("bagItem" + index, tempItemInfo);
            index++;
        }
        index = 1;
        foreach (var item in _equipsData)
        {
            json.Add("weapon" + index, item.Value);
            index++;
        }
        return json.ToString();
    }

    //弹出消息框
    public void ShowMessageByCode(string _type, int _code)
    {
        string message = messageInfos.GetField("MSG_" + _type + "_" + _code).GetValue(null).ToString();
        UIManager.GetInstance().ShowMessage(message);
    }
    //处理ERRORCODE
    int GetErrorCode(string _jsonText)
    {
        JsonData data = JsonMapper.ToObject(_jsonText);
        return (int)data["errorcode"];
    }
}


