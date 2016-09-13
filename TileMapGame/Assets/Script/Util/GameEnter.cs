using UnityEngine;
using System.Collections;

public class GameEnter : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		InitPool ();
	}
	
	// Update is called once per frame
	void InitPool ()
	{
		ObjectPoolManager.Instance.CreatePool (ObjectPoolType.EffectBomb.ToString(), 5, 10, Resources.Load ("Prefab/Effect/EffectBomb") as GameObject);
		ObjectPoolManager.Instance.CreatePool (ObjectPoolType.EntityBomb.ToString(), 5, 10, Resources.Load ("Prefab/Entity/EntityBomb") as GameObject);
	}
}

