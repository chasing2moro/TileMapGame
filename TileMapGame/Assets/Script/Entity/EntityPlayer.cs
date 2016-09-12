using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityPlayer : EntityMoveEnable
{
	void OnEnable(){
		RegistEvent (GameEvent.m_EventInput.InteractMapTap, OnHandleInteractMap);
	}

	void OnDisable(){
		UnRegistEvent (GameEvent.m_EventInput.InteractMapTap, OnHandleInteractMap);
	}


	#region Message
	//请求计算路线
	object OnHandleInteractMap(object vSender){
		FindPath.Instance.FindingPath (this.gameObject, (Vector2)vSender);
		return null;
	}

	//请求计算路线返回
	public void OnHandleMessageMove(List<Grid.NodeItem> vPathList){
		//缓存路线
		if (_moveBase != null)
			((MovePath)_moveBase).m_PathList = vPathList;

		//进入移动状态
		EnterState (EntityState.Walk);
	}
	#endregion
}

