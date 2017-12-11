using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

/// <Summary>
/// Adds a "Edit Prefab" option in the Assets menu (or right clicking an asset in the project browser).
/// This opens an empty scene with your prefab where you can edit it.
/// Put this script in your project as Assets/Editor/EditPrefab.cs
/// Developed by ulrikdamm.
/// </Summary>

public class EditPrefab {
	static Object getPrefab(Object selection) {
		var prefabType = PrefabUtility.GetPrefabType(selection);
		if (prefabType == PrefabType.PrefabInstance) {
			var prefab = PrefabUtility.GetPrefabParent(selection);
			return prefab;
		}
		
		if (prefabType == PrefabType.Prefab) {
			return selection;
		}
		
		return null;
	}
	
	[MenuItem("Assets/Edit prefab")]
	public static void editPrefab() {
		EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		
		var currentScene = EditorSceneManager.GetActiveScene().path;
		
		var prefab = getPrefab(Selection.activeObject);
		if (prefab == null) { Debug.Log("Couldn't find prefab"); return; }
		
		var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
		var instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, scene);
		Selection.activeObject = instance;
		SceneView.lastActiveSceneView.FrameSelected();
		
		var returnButton = new ReturnToSceneGUI();
		returnButton.previousScene = currentScene;
		returnButton.prefabInstance = instance;
	}
}

public sealed class ReturnToSceneGUI {
	public string previousScene;
	public GameObject prefabInstance;
	
	public ReturnToSceneGUI() {
		SceneView.onSceneGUIDelegate += RenderSceneGUI;
	}
	
	public void RenderSceneGUI(SceneView sceneview) {
		var style = new GUIStyle();
		style.margin = new RectOffset(10, 10, 10, 10);
		
		Handles.BeginGUI();
		GUILayout.BeginArea(new Rect(20, 20, 180, 300), style);
		var rect = EditorGUILayout.BeginVertical();
		GUI.Box(rect, GUIContent.none);
		
		if (GUILayout.Button("Apply and return", new GUILayoutOption[0])) {
			PrefabUtility.ReplacePrefab(prefabInstance, PrefabUtility.GetPrefabParent(prefabInstance), ReplacePrefabOptions.ConnectToPrefab);
			SceneView.onSceneGUIDelegate -= RenderSceneGUI;
			EditorSceneManager.OpenScene(previousScene);
		}
		
		if (GUILayout.Button("Discard changes", new GUILayoutOption[0])) {
			SceneView.onSceneGUIDelegate -= RenderSceneGUI;
			EditorSceneManager.OpenScene(previousScene);
		}
		
		EditorGUILayout.EndVertical();
		GUILayout.EndArea();
		Handles.EndGUI();
	}
}
