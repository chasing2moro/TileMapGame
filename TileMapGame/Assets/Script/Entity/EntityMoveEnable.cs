using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityMoveEnable : EntityBase
{
	public override int GetGridX (){
		#warning Unimplement GetGridX
		return 0;
	}
	public override int GetGridY(){
		#warning Unimplement GetGridY
		return 0;
	}

	EntityState m_State;

	//走路组件
	protected MoveBase _moveBase;
	#region State
	public void EnterState(EntityState vState){
		m_State = vState;
		switch (m_State) {
		case EntityState.Idle:
			OnStateIdleOnce ();
			break;
		case EntityState.Walk:
			OnStateWalkOnce ();
			break;
		default:
			break;
		}
	}

	void OnStateIdleOnce(){
		Debug.Log ("idle:" + this.gameObject.name);
	}

	void OnStateIdle(){

	}

	void OnStateWalkOnce(){
		Debug.Log ("walk:" + this.gameObject.name);
		if (_moveBase != null)
			_moveBase.Move ();
	}

	void OnStateWalk(){

	}
	#endregion

	#region Unity Event
	protected virtual void Awake(){
		_moveBase = gameObject.GetAddComponent<MoveBase> ();
		_moveBase.m_MoveFinishCallBack = OnHandleMoveFinish;
	}

	protected virtual void OnDestroy(){
		_moveBase.m_MoveFinishCallBack = null;
	}
		
	protected virtual void Update(){
		switch (m_State) {
		case EntityState.Idle:
			OnStateIdle ();
			break;
		case EntityState.Walk:
			OnStateWalk ();
			break;
		default:
			break;
		}
	}
	#endregion

	#region Message
	void OnHandleMoveFinish(){
		//进入空闲状态
		EnterState (EntityState.Idle);
	}
	#endregion
}

