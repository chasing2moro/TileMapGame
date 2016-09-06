using UnityEngine;
using UnityEditor;
using System.Collections;

public class TileMapWindow : EditorWindow {
	public static Texture2D m_SelectTexture; 
	public static TileMapElemType m_SelectElemType;

	Texture2D _tempTexture;
	// Use this for initialization
	void OnGUI () {
		//Assign Texture
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField ("Texture Elem");
		_tempTexture = (Texture2D)EditorGUILayout.ObjectField (m_SelectTexture, typeof(Texture2D), true);
		EditorGUILayout.EndHorizontal ();

		//EstimateElemType
		if (_tempTexture != m_SelectTexture) {
			m_SelectElemType = EstimateElemType (_tempTexture);
		}
		m_SelectTexture = _tempTexture;

		//Assign Elem Type
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField ("Elem Type");
		m_SelectElemType = (TileMapElemType)EditorGUILayout.EnumPopup (m_SelectElemType);
		EditorGUILayout.EndHorizontal ();
	}
	
	// Update is called once per frame
	void OnDisable() {
		//save texture path to local
		if(m_SelectTexture != null)
			EditorPrefs.SetString ("TileMapWindow_SelectTexture_Path", AssetDatabase.GetAssetPath (m_SelectTexture));
	}

	void OnEnable(){
		//get texture from local
		if (m_SelectTexture == null) {
			string texturePath = EditorPrefs.GetString ("TileMapWindow_SelectTexture_Path", "");
			if (!string.IsNullOrEmpty (texturePath)) {
				m_SelectTexture = (Texture2D)AssetDatabase.LoadMainAssetAtPath (texturePath);
			}
		}

		m_SelectElemType = EstimateElemType (m_SelectTexture);
	}

	TileMapElemType _tempElemType;
	TileMapElemType EstimateElemType(Texture2D vTexture){
		for (int i = 0; i < (int)TileMapElemType.None; i++) {
			_tempElemType = (TileMapElemType)i;
			if (vTexture.name.Contains (_tempElemType.ToString ().ToLower())) {
				return _tempElemType;
			}
		}
		return TileMapElemType.None;
	}
}
