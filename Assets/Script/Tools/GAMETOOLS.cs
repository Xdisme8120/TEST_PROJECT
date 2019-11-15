using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
public class GAMETOOLS
{

    // Use this for initialization
    public static JsonData GetJson(string path)
    {
        string url = System.IO.Path.Combine(Application.streamingAssetsPath, path);
        string json = File.ReadAllText(url);
        return JsonMapper.ToObject(json);
    }

    public static int GetTypeNyID(int id)
    {
        return 0;
    }
    public static void SetCusor(bool _visable)
    {
        if (_visable)
        {
            Cursor.visible = _visable;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = _visable;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
