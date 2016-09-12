using UnityEngine;
using System.Collections;
using System;

public class MoveBase : Controller
{
	public float m_MoveSpeed = 5;
	public Action m_MoveFinishCallBack;

	protected bool _isShouldMove = false;
	public virtual void Move (){
		_isShouldMove = true;
	}
}

