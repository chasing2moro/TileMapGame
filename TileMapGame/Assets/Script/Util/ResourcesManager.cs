using UnityEngine;
using System.Collections;

public class ResourcesManager
{
	static ResourcesManager _instance;
	public static ResourcesManager Instance{
		get{
			if (_instance == null) {
				_instance = new ResourcesManager ();
			}
			return _instance;
		}
	}

	Object Load(string vPath){
		return Resources.Load(vPath);
	}

	void LoadAsync(string vPath){

	}
}

