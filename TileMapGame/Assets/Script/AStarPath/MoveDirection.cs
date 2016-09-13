using UnityEngine;
using System.Collections;

public class MoveDirection : MoveBase
{
	public Direction m_MoveDirection;

	// Update is called once per frame
	void Update ()
	{
		if (_isShouldMove) {
			transform.Translate (Vector3.left);
			if (m_MoveFinishCallBack != null)
				m_MoveFinishCallBack ();
			_isShouldMove = false;
		}
	}
}

