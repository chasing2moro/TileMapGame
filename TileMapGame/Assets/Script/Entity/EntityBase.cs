using UnityEngine;
using System.Collections;

public class EntityBase : Controller {
	Vector2 _tempVec;
	public  void GetGrid(out int vGridX, out int vGridY){
		_tempVec.x = transform.position.x;
		_tempVec.y = transform.position.y;
		TileMapUtil.GetGridFromPos (_tempVec, out vGridX, out vGridY);
	}
}

public enum EntityState{
	None,
	Idle,
	Walk,
	LayBomb
}