using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbilityController))]
[CanEditMultipleObjects]
public class AbilityControllerInspector : Editor
{
    SerializedProperty m_Ability1;
	SerializedProperty m_Ability1Key;

	private void OnEnable()
	{
		m_Ability1 = serializedObject.FindProperty("m_Ability1");
		m_Ability1Key = serializedObject.FindProperty("m_Ability1Key");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		GUIStyle testStyle = new GUIStyle();

		EditorGUILayout.BeginHorizontal();
	
		EditorGUILayout.PropertyField(m_Ability1);
		EditorGUILayout.Separator();
		EditorGUILayout.PropertyField(m_Ability1Key);

		
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		serializedObject.ApplyModifiedProperties();
	}
}
