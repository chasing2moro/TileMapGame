﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FindPath : MonoBehaviour {
	public static FindPath Instance;
	void Awake(){
		Instance = this;
	}
	private Grid grid;
	protected GameObject m_Planner;
	// Use this for initialization
	void Start () {
		grid = GetComponent<Grid> ();
	}
	
	// Update is called once per frame
	[ContextMenu("PathFind")]
	void PathFind () {
		FindingPath (grid.player.gameObject, new Vector2 (grid.destPos.position.x, grid.destPos.position.y));
	}

	public void FindingPath(GameObject vPlanner, Vector2 vTarget){
		m_Planner = vPlanner;
		FindingPath (new Vector2 (m_Planner.transform.position.x, m_Planner.transform.position.y),vTarget);
	}

	// A*寻路
	void FindingPath(Vector2 s, Vector2 e) {
		Grid.NodeItem startNode = grid.getItem (s);
		Grid.NodeItem endNode = grid.getItem (e);

		List<Grid.NodeItem> openSet = new List<Grid.NodeItem> ();
		HashSet<Grid.NodeItem> closeSet = new HashSet<Grid.NodeItem> ();
		openSet.Add (startNode);

		while (openSet.Count > 0) {

			//从 openSet 找出消耗最小的
			Grid.NodeItem curNode = openSet [0];
			for (int i = 0, max = openSet.Count; i < max; i++) {
				if (openSet [i].fCost <= curNode.fCost &&
				    openSet [i].hCost < curNode.hCost) {
					curNode = openSet [i];
				}
			}

			//消耗最小的Node从OpenSet移走,加入CloseSet
			openSet.Remove (curNode);
			closeSet.Add (curNode);

			// 找到的目标节点
			if (curNode == endNode) {
				generatePath (startNode, endNode);
				return;
			}

			// 判断周围节点，选择一个最优的节点
			foreach (var item in grid.getNeibourhood(curNode)) {
				// 如果是墙或者已经在关闭列表中
				if (item.isObstacle || closeSet.Contains (item))
					continue;
				// 计算当前相领节点现开始节点距离
				int newCost = curNode.gCost + getDistanceNodes (curNode, item);
				// 如果距离更小，或者原来不在开始列表中
				if (newCost < item.gCost || !openSet.Contains (item)) {
					// 更新与开始节点的距离
					item.gCost = newCost;
					// 更新与终点的距离
					item.hCost = getDistanceNodes (item, endNode);
					// 更新父节点为当前选定的节点
					item.parent = curNode;
					// 如果节点是新加入的，将它加入打开列表中
					if (!openSet.Contains (item)) {
						openSet.Add (item);
					}
				}
			}
		}

		generatePath (startNode, null);
	}

	// 生成路径
	void generatePath(Grid.NodeItem startNode, Grid.NodeItem endNode) {
		List<Grid.NodeItem> path = new List<Grid.NodeItem>();
		if (endNode != null) {
			Grid.NodeItem temp = endNode;
			while (temp != startNode) {
				path.Add (temp);
				temp = temp.parent;
			}
			// 反转路径
			path.Reverse ();
		}
		// 更新路径
		grid.updatePath(path);
		if (path.Count > 0) {
			m_Planner.SendMessage ("OnHandleMessageMove", path);
		}
	}

	// 获取两个节点之间的距离
	int getDistanceNodes(Grid.NodeItem a, Grid.NodeItem b) {
		int cntX = Mathf.Abs (a.x - b.x);
		int cntY = Mathf.Abs (a.y - b.y);
		// 判断到底是那个轴相差的距离更远
		if (cntX > cntY) {
			return 14 * cntY + 10 * (cntX - cntY);
		} else {
			return 14 * cntX + 10 * (cntY - cntX);
		}
	}


}
