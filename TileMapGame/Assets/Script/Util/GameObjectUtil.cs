using UnityEngine;
using System.Collections;

public static class GameObjectUtil {

	public static T GetAddComponent<T>(this GameObject vObj) where T : Component{
		T component = vObj.GetComponent<T> ();
		if (component == null) {
			vObj.AddComponent<T> ();
		}
		return component;
	}

	public static T GetAddComponent<T>(this Transform vObj) where T : Component{
		return vObj.gameObject.GetAddComponent<T> ();
	}
}
