using UnityEngine;
using System.Collections;

public class EntityBomb : EntityMoveDisable
{
	public int m_Len;
	 float m_BombTime = 1f;
	// Use this for initialization
	public void Bomb ()
	{
		int gridX;
		int gridY;
		GetGrid (out gridX, out gridY);

		 CreateEffectBomb (gridX, gridY);
		 CreateEffectBomb (gridX - 1, gridY);
		 CreateEffectBomb (gridX + 1, gridY);
		 CreateEffectBomb (gridX, gridY - 1);
		 CreateEffectBomb (gridX, gridY + 1);

		Animator animator = GetComponent<Animator> ();
		animator.SetTrigger ("WaitingForExplose");
		StartCoroutine (RecycleEntityBomb());
	}

	void CreateEffectBomb(int vGridX, int vGridY){
		//把bomb从对象池取出来
		GameObject effectBomb = ObjectPoolManager.Instance.Get (ObjectPoolType.EffectBomb.ToString());
		effectBomb.transform.SetParent (null, false);
		//指定位置
		Vector2 pos = TileMapUtil.GetPosFromGrid (vGridX, vGridY);
		effectBomb.transform.Set2DPosition (pos);
		//播放动画
		Animator animator = effectBomb.GetComponent<Animator> ();
		animator.SetTrigger ("Explosion");
		StartCoroutine (RecycleEffectBomb(effectBomb));
	}

	//回收
	IEnumerator RecycleEffectBomb(GameObject vEffectBomb){
		yield return new WaitForSeconds (m_BombTime);
		ObjectPoolManager.Instance.Release (ObjectPoolType.EffectBomb.ToString(),vEffectBomb );
	}

	//回收
	IEnumerator RecycleEntityBomb(){
		yield return new WaitForSeconds (m_BombTime + 0.1f);
		ObjectPoolManager.Instance.Release (ObjectPoolType.EntityBomb.ToString(), this.gameObject );
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}

