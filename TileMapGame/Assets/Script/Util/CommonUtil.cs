using UnityEngine;
using System.Collections;

public class CommonUtil : MonoBehaviour
{
	public static bool IsSameGrid(Vector2 vPosA, Vector2 vPosB){
		return TileMapUtil.GetGridFromPos (vPosA) == TileMapUtil.GetGridFromPos (vPosB);
	}
}

