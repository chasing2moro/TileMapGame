using UnityEngine;
using System.Collections;

public class EntityPlayer : EntityMoveEnable
{
	void OnEnable(){
		RegistEvent (GameEvent.m_EventInput.InteractMap, OnHandleInteractMap);
	}

	void OnDisable(){
		UnRegistEvent (GameEvent.m_EventInput.InteractMap, OnHandleInteractMap);
	}
	object OnHandleInteractMap(object vSender){
		FindPath.Instance.FindingPath (this.gameObject, (Vector2)vSender);
		return null;
	}
}

