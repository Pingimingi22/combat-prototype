using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Abilities;

[CustomEditor(typeof(AbilityController))]
[CanEditMultipleObjects]
public class AbilityControllerInspector : Editor
{
    SerializedProperty m_Ability1;
	SerializedProperty m_Ability1Key;

	SerializedProperty m_Ability2;
	SerializedProperty m_Ability2Key;

	SerializedProperty m_Ability3;
	SerializedProperty m_Ability3Key;

	private void OnEnable()
	{
		m_Ability1 = serializedObject.FindProperty("m_Ability1");
		m_Ability1Key = serializedObject.FindProperty("m_Ability1Key");

		m_Ability2 = serializedObject.FindProperty("m_Ability2");
		m_Ability2Key = serializedObject.FindProperty("m_Ability2Key");

		m_Ability3 = serializedObject.FindProperty("m_Ability3");
		m_Ability3Key = serializedObject.FindProperty("m_Ability3Key");

	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		GUIStyle testStyle = new GUIStyle();
		testStyle.normal.textColor = Color.white;
		testStyle.fontSize = 16;
		testStyle.alignment = TextAnchor.UpperCenter;
		EditorGUILayout.LabelField("Ability Controller", testStyle);
		EditorGUILayout.Separator();
		EditorGUILayout.Separator();
		EditorGUILayout.Separator();



		EditorGUIUtility.labelWidth = 75;


		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(m_Ability1);
		EditorGUILayout.PropertyField(m_Ability1Key);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(m_Ability2);
		EditorGUILayout.PropertyField(m_Ability2Key);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(m_Ability3);
		EditorGUILayout.PropertyField(m_Ability3Key);
		EditorGUILayout.EndHorizontal();

		//GUILayout.FlexibleSpace();

		serializedObject.ApplyModifiedProperties();
	}
}
