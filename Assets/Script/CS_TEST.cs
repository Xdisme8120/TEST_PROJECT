using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Json;
public class CS_TEST : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField Username;
    public InputField Password;
    public InputField RUsername;
    public InputField RPassword;
    public InputField RPasswordCheck;
    static int j = 0;
    private void Start()
    {
        //Debug.Log(SaveHeroJson());
        //SaveHero();
        //Login();
        //Register();
        //ChangePassword();
        // CreateHero("createHero","xdisyou");
        SaveHero();
    }
    IEnumerator EGetHeroInfo()
    {
        WWWForm form = new WWWForm();
        form.AddField("a", "getAllHero");
        form.AddField("heroName", "bitch");
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
    //登陆
    public void Login()
    {
        StartCoroutine("ELogin");
    }
    //注册
    public void Register()
    {

        StartCoroutine(ERigister());

    }
    public void CreateHero(string _action, string _username)
    {
        StartCoroutine(ECreaterHero(_action, _username));
    }
    //英雄信息存储
    void SaveHero()
    {
        string json = SaveHeroJson();
        Debug.Log(json);
        for (int i = 0; i < 3; i++)
        {
            j = i;
            switch (i)
            {
                case 0:
                    StartCoroutine(POST("updateBagInfo", "bitch", json, j));
                    break;
                case 1:
                    StartCoroutine(POST("updateCharInfo", "bitch", json, j));
                    break;
                case 2:
                    StartCoroutine(POST("updateItemInfo", "bitch", json, j));
                    break;
            }
        }
    }
    //修改密码
    void ChangePassword()
    {
        StartCoroutine("EChangePassword");
    }
    //修改密码的协程
    IEnumerator EChangePassword()
    {
        WWWForm form = new WWWForm();
        form.AddField("a", "updateUser");
        form.AddField("username", "xdisyou");
        form.AddField("oldpassword", "xdisyou");
        form.AddField("newpassword", "xdisme");
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
    //登陆的协程
    IEnumerator ELogin()
    {
        WWWForm form = new WWWForm();
        form.AddField("a", "login");
        form.AddField("username", "xdisyou");
        form.AddField("password", "xdisyou");
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

    //账号注册的协程
    IEnumerator ERigister()
    {
        WWWForm form = new WWWForm();
        form.AddField("a", "register");
        form.AddField("username", "xdisyou");
        form.AddField("password", "xdisyou");
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
    //英雄创建的协程
    IEnumerator ECreaterHero(string _action, string _username)
    {
        WWWForm form = new WWWForm();
        form.AddField("a", _action);
        form.AddField("username", _username);
        form.AddField("nikename", CreateHeroJson("swordman", "bitch"));
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

    IEnumerator POST(string _action, string _heroname, string _json, int _j)
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
            Debug.Log(_j.ToString() + "----" + www.text);
        }
    }

    // 创建英雄是存储的JSon
    public string CreateHeroJson(string _heroTypeName, string _nickName)
    {

        JsonObject json = new JsonObject();
        json.Add(_heroTypeName, _nickName);
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
            json.Add("bagItem" + i, -1);
        }
        for (int i = 1; i <= 6; i++)
        {
            json.Add("weapon" + i, -1);
        }

        return json.ToString();
    }
}
