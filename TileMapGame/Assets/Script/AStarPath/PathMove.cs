using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathMove : MonoBehaviour {
	public int m_MoveIndex;

	//走路路线
	public List<Grid.NodeItem> m_PathList;
	public float m_MoveSpeed = 5;
	bool _isShowMove = false;

	public Action m_MoveFinishCallBack;

	public void Move(List<Grid.NodeItem> vPathList){
		Move ();
		m_PathList = vPathList;
	}

	public void Move(){
		_isShowMove = true;
		m_MoveIndex = 0;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (_isShowMove) {
			//可以移动
			Grid.NodeItem node = m_PathList [m_MoveIndex];
			Vector2 position = transform.position;
			if (Vector2.Distance (node.pos, position) < 0.1f) {
				//距离够近，可以下一步
				if (m_MoveIndex == m_PathList.Count - 1) {
					//走到尽头
					_isShowMove = false;
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
