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
        EventCenter.AddListener<string>(EventDefine.GetHeroInfo, GetHeroInfo);
        EventCenter.AddListener<string>(EventDefine.GetHeroListInfo, GetHeroListInfo);
        EventCenter.AddListener<string, string>(EventDefine.Login, Login);
        EventCenter.AddListener<string, string>(EventDefine.CreateHero, CreateHero);
        EventCenter.AddListener<string, string, string>(EventDefine.Register, Register);
        EventCenter.AddListener<string, string, string>(EventDefine.ChangePassword, ChangePassword);
        EventCenter.AddListener<HeroState, Dictionary<int, GridInfo>, Dictionary<int, int>>(EventDefine.SaveHeroInfo, SaveHeroInfo);

    }
    private void Start()
    {
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
            UIManager.GetInstance().ShowMessage("请输入用户名密码");
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
            //            Debug.Log("wait");
        }
        yield return www;
        if (www.error != null)
        {
            UIManager.GetInstance().ShowMessage("服务器未响应");
            Debug.Log(www.error);
        }
        else
        {
            int code = GetErrorCode(www.text);
            if (code != 0)
            {
                UIManager.GetInstance().ShowMessage("登陆失败");
                //ShowMessageByCode("Login", code);
            }
            else
            {
                GameSystem.Instance.gamingDataController.SetUsername(_username);
                //获取list列表
                EventCenter.Broadcast(EventDefine.GetHeroListInfo, GamingData.username);
                //TODO:去GamingData内设置heroList数组


                UIManager.GetInstance().CloseUIForms("Login");
                isLogined = true;
                //登陆成功
                UIManager.GetInstance().ShowMessage("登陆成功");

            }
        }
    }
    /////////////////////////////////////////////////////

    //注册///////////////////////////////////////////////
    public void Register(string _username, string _password, string _checkPassword)
    {
        if (_password != _checkPassword)
        {
            UIManager.GetInstance().ShowMessage("两次密码不一样");
            //TODO 提示信息两次密码不一样
            return;
        }
        StartCoroutine(ERigister(_username, _password));
    }
    IEnumerator ERigister(string _username, string _password)
    {
        //Debug.Log(_username);
        //Debug.Log(_password);
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
            UIManager.GetInstance().ShowMessage("连接服务器失败");
            Debug.Log(www.error);
        }
        else
        {
            if (GetErrorCode(www.text) == 2)
            {
                UIManager.GetInstance().ShowMessage("用户已存在");
            }
            if (GetErrorCode(www.text) == 0)
            {
                UIManager.GetInstance().ShowMessage("创建用户成功");
            }
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
            if (GetErrorCode(www.text) == 1)
            {
                UIManager.GetInstance().ShowMessage("原密码输入错误");
            }
            if (GetErrorCode(www.text) == 0)
            {
                UIManager.GetInstance().ShowMessage("密码修改成功");
            }
            Debug.Log(www.text);
        }
    }
    /////////////////////////////////////////////////////

    //创建新英雄//////////////////////////////////////////
    public void CreateHero(string _heroType, string _nickName)
    {
        //Debug.Log(_nickName);
        StartCoroutine(ECreaterHero(_heroType, _nickName));
    }

    IEnumerator ECreaterHero(string _heroType, string _nickName)
    {
        WWWForm form = new WWWForm();
        form.AddField("a", "createHero");
        form.AddField("username", GamingData.username);
        form.AddField("nikename", CreateHeroJson(_heroType, _nickName));
        WWW www = new WWW("49.232.47.199/server/index.php", form);
        while (!www.isDone)
        {

            Debug.Log("wait");
        }
        yield return www;
        if (www.error != null)
        {
            UIManager.GetInstance().ShowMessage("相同英雄已存在");
            Debug.Log(www.error);
        }
        else
        {

            if (GetErrorCode(www.text) == 1)
            {
                UIManager.GetInstance().ShowMessage("英雄昵称已存在");
            }
            if (GetErrorCode(www.text) == 0)
            {
                UIManager.GetInstance().ShowMessage("创建英雄成功");
            }
            Debug.Log(www.text);
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
        form.AddField("heroName", _nickName);
        WWW www = new WWW("49.232.47.199/server/index.php", form);
        while (!www.isDone)
        {
            //            Debug.Log("wait");
        }
        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log(www.text);
            //将收到的英雄信息传给数据处理系统
            Debug.Log(www.text);
            system.gamingDataController.InitData(JsonMapper.ToObject(www.text));
            GameSystem.Instance.SetNewSceneState(new GameSceneState(GameSystem.Instance.sceneStateController), 1);
        }
        yield return null;
    }

    //获取已有英雄List列表信息//////////////////////////////(//////////
    public void GetHeroListInfo(string userName)
    {
        StartCoroutine(EGetHeroListInfo(userName));
    }
    IEnumerator EGetHeroListInfo(string userName)
    {
        WWWForm form = new WWWForm();
        form.AddField("a", "getsUserInfo");
        form.AddField("username", userName);
        WWW www = new WWW("49.232.47.199/server/index.php", form);
        while (!www.isDone)
        {
            //            Debug.Log("wait");
        }
        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            //TODO:处理通过username返回的英雄list列表
            GameSystem.Instance.gamingDataController.SetHeroList
                (JsonMapper.ToObject(JsonMapper.ToObject(www.text)["result"].ToJson()));
            UIManager.GetInstance().ShowUIForms("SelectHero");
            //将收到的英雄信息传给数据处理系统
            //system.gamingDataController.InitData(JsonMapper.ToObject(www.text));
            //GameSystem.Instance.gamingDataController.SetNickname(_nickName);
        }
        yield return null;
    }

    //存储英雄信息到服务器////////////////////////////////////////////
    public void SaveHeroInfo(HeroState _stateData, Dictionary<int, GridInfo> _invenData, Dictionary<int, int> _equipsData)
    {
        string json = SaveHeroJson(_stateData, _invenData, _equipsData, GamingData.synData);
        Debug.Log("存储的Json" + json);
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    StartCoroutine(POST("updateBagInfo", GamingData.nickname, json));
                    break;
                case 1:
                    StartCoroutine(POST("updateCharInfo", GamingData.nickname, json));
                    break;
                case 2:
                    StartCoroutine(POST("updateItemInfo", GamingData.nickname, json));
                    break;
                case 3:
                    StartCoroutine(POST("updateTaskInfo", GamingData.nickname, json));
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
            // Debug.Log("wait");
        }
        yield return www;
        if (www.error != null)
        {
            Debug.Log("ERROR" + Time.time);
        }
        else
        {
            Debug.Log(www.text);
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
    public string SaveHeroJson(HeroState _stateData, Dictionary<int, GridInfo> _invenData, Dictionary<int, int> _equipsData, SynData _synData)
    {
        Dictionary<int, string> tempInvenInfo = new Dictionary<int, string>();
        Dictionary<int, int> tempEquipInfo = new Dictionary<int, int>();
        JsonObject json = new JsonObject();
        // 英雄数据存储
        json.Add("level", _stateData.level);
        json.Add("CurrExp", _stateData.cueeExp);
        json.Add("Hp", (int)_stateData.hp);
        json.Add("Mp", (int)_stateData.sp);
        //背包数据存储
        int index = 1;
        foreach (var item in _invenData)
        {
            string tempItemInfo;
            if (item.Value.GetItemID() != -1)
            {
                if (item.Value.item.ItemType == 2)
                    tempItemInfo = item.Value.item.ID + "|" + 1.ToString();
                else
                {
                    tempItemInfo = item.Value.item.ID + "|" + item.Value.itemCount;
                }
            }
            else
            {
                tempItemInfo = "-1";
            }
            json.Add("bagItem" + index, tempItemInfo);
            index++;
        }
        //装备栏存储
        index = 1;
        foreach (var item in _equipsData)
        {
            json.Add("weapon" + index, item.Value);
            index++;
        }
        //任务信息存储 
        json.Add("taskID", _synData.taskID);
        json.Add("taskName", _synData.TaskName);
        json.Add("taskState", _synData.taskState);
        index = 1;
        string tempNpcID = "";
        string tempNpcState = "";
        foreach (var item in _synData.npcState.Keys)
        {
            tempNpcID += (item.ToString() + ((index == _synData.npcState.Count) ? "" : "|"));
            tempNpcState += (_synData.npcState[item].ToString() + ((index == _synData.npcState.Count) ? "" : "|"));
            index++;
        }
        json.Add("NpcID", tempNpcID);
        json.Add("NpcState", tempNpcState);
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


