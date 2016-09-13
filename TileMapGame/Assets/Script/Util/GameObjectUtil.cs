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

	static Vector2 _tempVec2;
	public static Vector2 Get2DPosition(this Transform vObj){
		_tempVec2.x = vObj.position.x;
		_tempVec2.y = vObj.position.y;
		return _tempVec2;
	}

	static Vector3 _tempVec3;
	public static void Set2DPosition(this Transform vObj, Vector2 vPos){
		_tempVec3.x = vPos.x;
		_tempVec3.y = vPos.y;
		_tempVec3.z = vObj.position.z;
		vObj.position = _tempVec3;
	}
}

