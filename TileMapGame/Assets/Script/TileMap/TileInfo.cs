using UnityEngine;
using System.Collections;

public class TileInfo : MonoBehaviour{
	public TileMapElemType m_ElemType;
	public int m_GridX;
	public int m_GridY;

	private void OnDrawGizmos()
	{
		Gizmos.color = TileMapUtil.GetDebugColor (m_ElemType);
		Gizmos.DrawSphere(transform.position, 0.2f);
	}
}
