using UnityEngine;
using System.Collections;
using System;
using CBX.TileMapping.Unity;

public class TileMapUtil {
	public Vector2 GetPosFromGrid(int vGridx, int vGridY){
		return Vector2.zero;
	}

	int _tempGridX;
	int _tempGridY;
	public Vector2 GetGridFromPos(Vector2 vPos){
		GetGridFromPos (vPos, out _tempGridX, out _tempGridY);
		return new Vector2 (_tempGridX, _tempGridY);
	}

	float GetTileWidth(){
		return TileMap.Current.TileWidth;
	}
	float GetTileHeight(){
		return TileMap.Current.TileHeight;
	}
	int GetRows(){
		return TileMap.Current.Rows;
	}
	int GetColumns(){
		return TileMap.Current.Columns;
	}

	public void GetGridFromPos(Vector2 vPos, out int vGridX, out int vGridY){
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
}
