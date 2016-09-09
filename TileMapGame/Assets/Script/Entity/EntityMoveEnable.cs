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
	PathMove m_PathMove;
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
		Debug.Log ("idle");
	}

	void OnStateIdle(){

	}

	void OnStateWalkOnce(){
		Debug.Log ("walk");
		if (m_PathMove != null)
			m_PathMove.Move ();
	}

	void OnStateWalk(){

	}
	#endregion

	#region Unity Event
	protected virtual void Awake(){
		m_PathMove = gameObject.GetAddComponent<PathMove> ();
		m_PathMove.m_MoveFinishCallBack = OnHandleMoveFinish;
	}

	protected virtual void OnDestroy(){
		m_PathMove.m_MoveFinishCallBack = null;
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
	//请求移动
	public void OnHandleMessageMove(List<Grid.NodeItem> vPathList){
		//缓存路线
		if (m_PathMove != null)
			m_PathMove.m_PathList = vPathList;

		//进入移动状态
		EnterState (EntityState.Walk);
	}

	void OnHandleMoveFinish(){
		//进入空闲状态
		EnterState (EntityState.Idle);
	}
	#endregion
}

