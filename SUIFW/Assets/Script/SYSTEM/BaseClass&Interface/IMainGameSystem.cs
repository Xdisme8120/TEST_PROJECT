using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMainGameSystem{

	protected InGameSystem inGameSystem;
	public IMainGameSystem(InGameSystem _inGameSystem)
	{
		inGameSystem = _inGameSystem;
	}
	public abstract void Init();
	public abstract void Update();
	public abstract void Release();
}
