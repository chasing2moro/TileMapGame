using UnityEngine;
using System.Collections;

public abstract class EntityBase : MonoBehaviour {
	public abstract int GetGridX ();
	public abstract int GetGridY();
}

public enum EntityState{
	None,
	Idle,
	Walk
}