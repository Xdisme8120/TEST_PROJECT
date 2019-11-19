using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneStateController:BaseSystemController
{
    SceneState m_State;
    private bool m_bRunBegin = false;
    public int currSceneIndex;
    public SceneStateController(GameSystem _system):base(_system) { }
    public void SetState(SceneState state, int sceneIndex)
    {
        m_bRunBegin = false;
        if (m_State != null)
        {
            m_State.StateEnd();
        }
        LoadScene(sceneIndex);

        m_State = state;
    }
    //主系统中Update一直调用的函数
    public void StateUpdate()
    {
        //如果正在加载场景直接返回
        if (Application.isLoadingLevel)
            return;
        //执行update
        if (m_State != null && m_bRunBegin == false)
        {
            m_State.StateBegin();
            m_bRunBegin = true;
        }
        if (m_State != null)
        {
            m_State.StateUpdate();
        }
    }
    //无状态改变下切换场景
    public void LoadSceneNoState(int sceneIndex)
    {
        if (sceneIndex == -1)
            return;
        currSceneIndex = sceneIndex;
        SceneManager.LoadScene(sceneIndex);
    }
    //切换场景
    void LoadScene(int sceneIndex)
    {
        Debug.Log("套套");
        if (sceneIndex == -1)
            return;
        currSceneIndex = sceneIndex;
        SceneManager.LoadScene(sceneIndex);
    }




}
