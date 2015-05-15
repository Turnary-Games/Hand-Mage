using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CameraControl))]
public class _CameraControl : Editor {

	private bool autoMoveCamera = true;

	public override void OnInspectorGUI() {
		CameraControl script = (CameraControl)target;

		#region Basic variables
		script.player = (GameObject)EditorGUILayout.ObjectField ("Player", script.player, typeof(GameObject), true);
		script.hand = (GameObject)EditorGUILayout.ObjectField ("Hand", script.hand, typeof(GameObject), true);

		EditorGUILayout.Space ();

		script.cameraSpeed = EditorGUILayout.FloatField ("Camera speed", script.cameraSpeed);
		script.zoomSpeed = EditorGUILayout.FloatField ("Zoom speed", script.zoomSpeed);

		EditorGUILayout.Space ();

		script.handFocusAmount = EditorGUILayout.Slider ("Hand focus " + Mathf.Floor (script.handFocusAmount * 100).ToString () + "%", script.handFocusAmount, 0, 1);
		string label = "ERROR";
		if (script.handFocusAmount == 0) // 0% = x
			label = "Camera will ignore the hand";
		else if (script.handFocusAmount <= 0.3f) // 0% < x <= 30%
			label = "Camera will barely follow";
		else if (script.handFocusAmount < 0.7f) // 30% < x < 70%
			label = "Camera will follow to a modern degree";
		else if (script.handFocusAmount < 1f) // 70% <= x < 100%
			label = "Camera will almost ignore the player";
		else if (script.handFocusAmount == 1f) // x = 100%
			label = "Camera will ignore the player";
		EditorGUILayout.LabelField (label);

		EditorGUILayout.Space ();
		#endregion

		GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});

		#region Move camera inside bounds
		autoMoveCamera = EditorGUILayout.Foldout (autoMoveCamera, "Move camera inside bounds");

		if (autoMoveCamera) {
			EditorGUI.indentLevel++;

			GUI.skin.label.wordWrap = true;
			EditorGUILayout.LabelField ("Jump into a bound, but only when you click the button. Purely for mapmaking and testing purposes.");

			EditorGUILayout.Space ();
			script.tmpBounds = (BoxCollider2D)EditorGUILayout.ObjectField ("Bound", script.tmpBounds, typeof(BoxCollider2D), true);
			EditorGUILayout.Space ();

			if (GUILayout.Button ("Auto select bound")) {
				foreach(BoundsTrigger trigger in FindObjectsOfType<BoundsTrigger>()) {
					if (trigger.bounds != null && trigger.triggerOnStart) {
						script.tmpBounds = trigger.bounds;
						break;
					}
				}
			}
			if (GUILayout.Button ("Move inside selected")) {
				script.GoInsideTempBounds ();
			}
			
			EditorGUI.indentLevel--;
		}
		#endregion
	}

}

