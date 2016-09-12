using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 每个要移动的Entity都要挂接这个脚本
/// </summary>
public class MovePath : MoveBase {
	public int m_MoveIndex;

	//走路路线
	public List<Grid.NodeItem> m_PathList;

//	public void Move(List<Grid.NodeItem> vPathList){
//		Move ();
//		m_PathList = vPathList;
//	}

	public override void Move(){
		m_MoveIndex = 0;
		base.Move ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (_isShouldMove) {
			//可以移动
			Grid.NodeItem node = m_PathList [m_MoveIndex];
			Vector2 position = transform.position;
			if (Vector2.Distance (node.pos, position) < 0.1f) {
				//距离够近，可以下一步
				if (m_MoveIndex == m_PathList.Count - 1) {
					//走到尽头
					_isShouldMove = false;
					if (m_MoveFinishCallBack != null)
						m_MoveFinishCallBack ();
				}
				m_MoveIndex++;
			} else {
				transform.position = Vector2.MoveTowards (position, node.pos, m_MoveSpeed * Time.deltaTime);
			}
		}
	}
}
