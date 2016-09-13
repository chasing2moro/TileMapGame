using UnityEngine;
using System.Collections;

public class EntityBomb : EntityMoveDisable
{
	public int m_Len;
	// Use this for initialization
	void Start ()
	{
		GameObject effectBomb = ObjectPoolManager.Instance.Get ("EffectBomb");
		Animator animator = effectBomb.GetComponent<Animator> ();
		animator.SetTrigger ("Explosion");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

