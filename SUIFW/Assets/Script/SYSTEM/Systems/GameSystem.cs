using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
public class GameSystem : MonoBehaviour
{

    static GameSystem instance;//系统单例
    public static bool isFirstInitSystem = false;//是否时首次打开系统
    public static GameSystem Instance
    {
        get { return instance; }
    }

    SceneStateController sceneStateController;//场景状态控制
    GamingDataController gamingDataController;//数据控制器
    CSInertactiveW cSInertactive;//客户端服务段交互控制
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        //获取客户端服务端交互组件
        gameObject.AddComponent(typeof(CSInertactiveW));
        //初始化场景状态控制器
        sceneStateController = new SceneStateController(this);
        //初始化游戏数据控制器
        gamingDataController = new GamingDataController(this);
        //打开游戏自动进入登陆场景状态
        sceneStateController.SetState(new LoginSceneState(sceneStateController), -1);
    }
    void Start()
    { }
    void Update()
    {
        //sceneStateUpdate函数调用
        sceneStateController.StateUpdate();
    }
    //返回登陆状态
    public bool GetLoginState()
    {
        return cSInertactive.IsLogined;
    }
}
