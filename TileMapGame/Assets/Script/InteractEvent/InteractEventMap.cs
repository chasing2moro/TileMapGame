using UnityEngine;
using System.Collections;

public class InteractEventMap : Controller {
	void OnTap(TapGesture gesture) { 
		Vector3 tapMapPos = CameraHelper.ScreenPosToBackgroundPos (gesture.Position);
		//Debug.Log("tap Obj pos:" + tapMapPos);
		SendEvent (GameEvent.m_EventInput.InteractMap, new Vector2(tapMapPos.x, tapMapPos.y));
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
