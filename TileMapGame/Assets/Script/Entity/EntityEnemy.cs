using UnityEngine;
using System.Collections;

public class EntityEnemy : EntityMoveEnable
{

	// Use this for initialization
	void Start ()
	{
		Invoke ("RandomMove", 3);
	}

	void RandomMove(){
		((MoveDirection)_moveBase).m_MoveDirection = GetRandomDirection ();

		//进入移动状态
		EnterState (EntityState.Walk);
	}

	Direction GetRandomDirection(){
		return (Direction)Random.Range (0, (int)Direction.Max);
	}
}

