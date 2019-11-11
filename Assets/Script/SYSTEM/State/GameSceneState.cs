using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneState : SceneState
{
    //游戏内部系统
    InGameSystem inGameSystem;
    public GameSceneState(SceneStateController controller) : base(controller)
    {}

    public override void StateBegin()
    {
        inGameSystem = new InGameSystem();
    }
    public override void StateUpdate()
    {
        //inGameSystem.Update();
    }
    public override void StateEnd()
    {
        //inGameSystem.Release();
    }
    //初始化子系统
}
