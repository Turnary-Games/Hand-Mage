#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BoundsTrigger))]
public class _BoundsTrigger : Editor {

	public override void OnInspectorGUI() {
		BoundsTrigger script = (BoundsTrigger)target;

		// Trigger option: Enum
		script.onTrigger = (BoundsTrigger.Option)EditorGUILayout.EnumPopup ("Action on trigger", script.onTrigger);

		ViewOptions (ref script);
	}

	void ViewOptions(ref BoundsTrigger script) {

		// View the rest depending on the trigger choice
		GUI.enabled = script.onTrigger != BoundsTrigger.Option.disableBounds;
		EditorGUILayout.Foldout (GUI.enabled, "Options");
		if (GUI.enabled) {
			EditorGUI.indentLevel++;

			ViewTriggerOnStart(ref script);
			ViewBounds(ref script);
			ViewZoom(ref script);
			
			EditorGUI.indentLevel--;
		}
	}

	#region Options

	void ViewTriggerOnStart(ref BoundsTrigger script) {
		// Trigger on start: Boolean
		script.triggerOnStart = EditorGUILayout.Toggle ("Trigger on start", script.triggerOnStart);
	}

	void ViewBounds(ref BoundsTrigger script) {
		script.bounds = (BoxCollider2D)EditorGUILayout.ObjectField ("Bounds", script.bounds, typeof(BoxCollider2D), true);
	}

	void ViewZoom(ref BoundsTrigger script) {
		script.zoom = EditorGUILayout.Toggle ("Zoom", script.zoom);

		bool enabled = GUI.enabled; GUI.enabled = script.zoom;
		script.zoomAmount = EditorGUILayout.FloatField ("Zoom amount", script.zoomAmount);
		GUI.enabled = enabled;

		EditorGUILayout.LabelField("Camera currently set to " + Camera.main.orthographicSize.ToString());
	}

	#endregion

}

#endif