using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public abstract class SceneState
{
	//系统控制器
	protected SceneStateController m_Controller;
    private string m_StateName = "MenuState";
    public string StateName
    {
        get { return m_StateName; }
        set { m_StateName = value; }
    }

	public SceneState(SceneStateController controller)
	{
		m_Controller = controller;
	}
	//状态更新完成
	public virtual void StateBegin()
	{}
	//状态结束
	public virtual void StateEnd()
	{}
	//状态
	public virtual void StateUpdate()
	{}
}
