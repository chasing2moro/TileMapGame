using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityPlayer : EntityMoveEnable
{
	#region State
	public override void EnterState(EntityState vState){
		base.EnterState (vState);
		switch (_state) {
		case EntityState.LayBomb:
			OnStateLayBombOnce ();
			break;
		default:
			break;
		}
	}

	void OnStateLayBombOnce(){
		GameObject bomb = ObjectPoolManager.Instance.Get (ObjectPoolType.EntityBomb.ToString());
		bomb.transform.Set2DPosition (this.transform.Get2DPosition ());
	}

	void OnStateLayBomb(){

	}
		
	#endregion
	#region Unity Event
	void OnEnable(){
		RegistEvent (GameEvent.m_EventInput.InteractMapTap, OnHandleInteractMap);
	}

	void OnDisable(){
		UnRegistEvent (GameEvent.m_EventInput.InteractMapTap, OnHandleInteractMap);
	}

	protected virtual void Update(){
		base.Update ();
		switch (_state) {
		case EntityState.LayBomb:
			OnStateLayBomb ();
			break;
		default:
			break;
		}
	}
	#endregion

	#region Message
	//请求计算路线
	object OnHandleInteractMap(object vSender){
		if (CommonUtil.IsSameGrid (transform.Get2DPosition (), (Vector2)vSender)) {
			Debug.Log ("Tap me");
			EnterState (EntityState.LayBomb);
			return null;
		}
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

