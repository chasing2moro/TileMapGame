using UnityEngine;
using System.Collections;
using System;
using CBX.TileMapping.Unity;

public class TileMapUtil {
	public static Vector2 GetPosFromGrid(int vGridx, int vGridY){
		return new Vector2 (vGridx * GetTileWidth () + GetTileWidth () / 2, vGridY * GetTileHeight () + GetTileHeight () / 2);
	}

	static int _tempGridX;
	static int _tempGridY;
	public static Vector2 GetGridFromPos(Vector2 vPos){
		GetGridFromPos (vPos, out _tempGridX, out _tempGridY);
		return new Vector2 (_tempGridX, _tempGridY);
	}

	public static float GetTileWidth(){
		return TileMap.Current.TileWidth;
	}

	public static float GetTileHeight(){
		return TileMap.Current.TileHeight;
	}

	public static int GetRows(){
		return TileMap.Current.Rows;
	}

	public static int GetColumns(){
		return TileMap.Current.Columns;
	}

	public static void GetGridFromPos(Vector2 vPos, out int vGridX, out int vGridY){
		// calculate column and row location from mouse hit location
		var pos = new Vector2(vPos.x / GetTileWidth(), vPos.y / GetTileHeight());

		// round the numbers to the nearest whole number using 5 decimal place precision
		// do a check to ensure that the row and column are with the bounds of the tile map
		var col = (int)Math.Round(pos.x, 5, MidpointRounding.ToEven);
		var row = (int)Math.Round (pos.y, 5, MidpointRounding.ToEven);
		if (row < 0)
		{
			row = 0;
		}

		if (row > GetRows() - 1)
		{
			row = GetRows() - 1;
		}

		if (col < 0)
		{
			col = 0;
		}

		if (col > GetColumns() - 1)
		{
			col = GetColumns() - 1;
		}

		vGridX = col;
		vGridY = row;
	}

	public static TileInfo GetTileInfoByGrid(int vGridX, int vGridY){
		Transform grid = TileMap.Current.transform.Find (string.Format ("Tile_{0}_{1}", vGridX, vGridY));
		if (grid == null)
			Debug.LogError ("gridx:" + vGridX + " gridy:" + vGridY + " can not find grid");
		return grid.GetComponent<TileInfo> ();
	}

	public static bool IsObstacle(TileMapElemType vElemType){
		return (int)vElemType > (int)TileMapElemType.Grass;
	}
}
