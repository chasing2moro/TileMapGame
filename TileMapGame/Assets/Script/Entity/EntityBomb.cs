﻿using UnityEngine;
using System.Collections;

public class EntityBomb : EntityMoveDisable
{
	 int m_Len = 5;
	 float m_BombTime = 2f;
	int _effectBombNum;
	int _effectBombCounter;
	// Use this for initialization
	public void Bomb ()
	{
		//获取炸弹的位置
		int gridX;
		int gridY;
		GetGrid (out gridX, out gridY);

		//创建爆炸效果
		_effectBombNum = 0;
		_effectBombCounter = 0;
		 EffectBombCreate (gridX, gridY);
		++_effectBombNum;
		for (int i = -m_Len; i <= m_Len; i++) {
			for (int j = -m_Len; j <= m_Len; j++) {
				if(i * j == 0){
					if(i == 0 && j == 0)
						continue;
					EffectBombCreate (gridX + i, gridY + j);
					++_effectBombNum;
				}
			}
		}

		//播放炸弹等待效果
		Animator animator = GetComponent<Animator> ();
		animator.SetTrigger ("WaitingForExplose");

	}

	void EffectBombCreate(int vGridX, int vGridY){
		//把bomb从对象池取出来
		GameObject effectBomb = ObjectPoolManager.Instance.Get (ObjectPoolType.EffectBomb.ToString());
		effectBomb.transform.SetParent (null, false);
		effectBomb.gameObject.SetActive (false);
		//指定位置
		Vector2 pos = TileMapUtil.GetPosFromGrid (vGridX, vGridY);
		effectBomb.transform.Set2DPosition (pos);
		//要爆炸
		StartCoroutine( EffectBombExplose (effectBomb, m_BombTime) );
	}

	IEnumerator EffectBombExplose(GameObject vEffectBomb, float vDelayTime){
		yield return new WaitForSeconds (vDelayTime);
		vEffectBomb.gameObject.SetActive (true);

		//播放动画
		Animator animator = vEffectBomb.GetComponent<Animator> ();
		animator.SetTrigger ("Explosion");

		//回收EffectBomb
		StartCoroutine (RecycleEffectBomb(vEffectBomb));
	}

	//回收EffectBomb
	IEnumerator RecycleEffectBomb(GameObject vEffectBomb){
		yield return new WaitForSeconds (0.7f);
		ObjectPoolManager.Instance.Release (ObjectPoolType.EffectBomb.ToString(),vEffectBomb );

		++_effectBombCounter;
		if (_effectBombCounter == _effectBombNum) {
			//回收EntityBomb
			RecycleEntityBomb();
		}
	}

	//回收EntityBomb
	void RecycleEntityBomb(){
		ObjectPoolManager.Instance.Release (ObjectPoolType.EntityBomb.ToString(), this.gameObject );
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}

