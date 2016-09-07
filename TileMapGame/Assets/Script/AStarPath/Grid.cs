//#define kDebugPath
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	// 节点半径
	public float NodeRadius = 0.5f;

	// 玩家
	public Transform player;
	// 目标
	public Transform destPos;


	/// <summary>
	/// 寻路节点
	/// </summary>
	public class NodeItem {
		// 是否是障碍
		public bool isObstacle;
		// 位置
		public Vector2 pos{
			get{
				if (_isChangePos) {
					_pos = TileMapUtil.GetPosFromGrid (x, y);
					_isChangePos = false;
				}
				return _pos;
			}
		}
		// 格子坐标
		public int x, y;

		// 不用下面
		bool _isChangePos = true;
		Vector2 _pos;

		// 与起点的长度
		public int gCost;
		// 与目标点的长度
		public int hCost;

		// 总的路径长度
		public int fCost {
			get {return gCost + hCost; }
		}

		// 父节点
		public NodeItem parent;

		public NodeItem(bool vsObsItacle, int x, int y) {
			this.isObstacle = vsObsItacle;
			this.x = x;
			this.y = y;
		}
	}

	private NodeItem[,] grid;
	private int w, h;

	private GameObject WallRange, PathRange;

	void Start() {
		Debug.Log( typeof(Animator).Assembly.FullName );

		// 初始化格子
		w = TileMapUtil.GetColumns(); 
		h = TileMapUtil.GetRows ();
		grid = new NodeItem[w, h];

		bool isObstacle;
		// 将墙的信息写入格子中
		for (int x = 0; x < w; x++) {
			for (int y = 0; y < h; y++) {
				// 构建一个节点
				TileInfo tileInfo = TileMapUtil.GetTileInfoByGrid(x, y);
				isObstacle = TileMapUtil.IsObstacle (tileInfo.m_ElemType);
				grid[x, y] = new NodeItem (isObstacle, x, y);
			}
		}

	}

	// 根据坐标获得一个节点
	public NodeItem getItem(Vector2 position) {
		int x;
		int y;
		TileMapUtil.GetGridFromPos(position, out x, out y);
		x = Mathf.Clamp (x, 0, w - 1);
		y = Mathf.Clamp (y, 0, h - 1);
		return grid [x, y];
	}

	// 取得周围的节点
	public List<NodeItem> getNeibourhood(NodeItem node) {
		List<NodeItem> list = new List<NodeItem> ();
		for (int i = -1; i <= 1; i++) {
			for (int j = -1; j <= 1; j++) {
				// 如果是自己，则跳过
				if (i == 0 && j == 0)
					continue;
				int x = node.x + i;
				int y = node.y + j;
				// 判断是否越界，如果没有，加到列表中
				if (x < w && x >= 0 && y < h && y >= 0)
					list.Add (grid [x, y]);
			}
		}
		return list;
	}

	#if kDebugPath
	List<GameObject> debugPathList;
	#endif
	// 更新路径
	public void updatePath(List<NodeItem> lines) {
		#if kDebugPath
		if (debugPathList == null) {
			debugPathList = new List<GameObject> ();
		}
		int debugCount = debugPathList.Count;
		for (int i = 0, max = lines.Count; i < max; i++) {
			GameObject obj = null;
			if(i < debugCount){
				obj = debugPathList [i];
				obj.SetActive (true);
			}else{
				obj = GameObject.CreatePrimitive (PrimitiveType.Cube);
				obj.transform.localScale = Vector3.one * 0.3f;
				debugPathList.Add (obj);
			}
			obj.name = (string.Format ("debugPath_{0}_{1}", lines [i].x, lines [i].y));
			obj.transform.localPosition = lines [i].pos;
		}

		for (int i = lines.Count; i < debugPathList.Count; i++) {
			debugPathList [i].SetActive (false);
		}
		#endif
	}
}
