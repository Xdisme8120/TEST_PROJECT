using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneState : SceneState
{
    //游戏内部系统
    InGameSystem inGameSystem;
    public GameSceneState(SceneStateController controller) : base(controller)
    {}

    //进入游戏状态的初始化
    public override void StateBegin()
    {
        inGameSystem = new InGameSystem();
        //inGameSystem.Init(inGameSystem.SynopsisSystem);
    }
    //子系统类Update函数调用
    public override void StateUpdate()
    {
        inGameSystem.Update(inGameSystem.HeroSystem);
        inGameSystem.Update(inGameSystem.SynopsisSystem);
    }
    //游戏状态结束
    public override void StateEnd()
    {
        inGameSystem.Release(inGameSystem.HeroSystem);
        inGameSystem.Release(inGameSystem.SynopsisSystem);
    }
    //初始化子系统
}
