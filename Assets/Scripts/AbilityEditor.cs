using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



namespace PrototypeEditorTools
{
    public enum AbilityBehaviour
    { 
        LUNGE,
        ICARUS,
        TELEPORT,
        EXPLOSION,
        SPEED,
        STRENGTH,
        DRAIN,
    }
    public class AbilityEditor : EditorWindow
    {
        // =========== Generic Settings =========== //
        private string m_name = "Default Ablitity";
        private AbilityBehaviour m_behaviour;
        // ======================================== //

        // =========== Lunge Settings =========== //
        private float m_LungeForce;
        // ====================================== //

        [MenuItem("Window/Ability Editor")]
        static void Init()
        {
            AbilityEditor window = (AbilityEditor)EditorWindow.GetWindow(typeof(AbilityEditor));
            window.Show();
            window.minSize = new Vector2(500, 500);
            window.maxSize = new Vector2(500, 500);
        }

        private void OnGUI()
        {
            GUIStyle testStyle = new GUIStyle();
            testStyle.fontSize = 35;
            testStyle.alignment = TextAnchor.UpperCenter;
            testStyle.fontStyle = FontStyle.Bold;
            testStyle.normal.textColor = Color.white;


            GUILayout.Label("Ability Creator", testStyle);
            GUILayout.Label("Ablitity Name:", EditorStyles.boldLabel);


            m_name = EditorGUILayout.TextField(m_name);
            m_behaviour = (AbilityBehaviour)EditorGUILayout.EnumPopup("Ability Behaviour:", m_behaviour);

            DisplaySpecificSettings(m_behaviour);

            if (GUILayout.Button("Create Ability"))
            {
                string path = "Assets/" + m_name + ".asset";

                string className = GetAbilityType(m_behaviour);
                CreateAbility(m_name, path, className);
            }
        }

        private void CreateAbility(string name, string path, string className)
        {
            ScriptableObject testAbility = ScriptableObject.CreateInstance(className);
            if (AssetDatabase.FindAssets(name).Length == 0)
            {
                Debug.Log("Created a new ability.");

                AssetDatabase.CreateAsset(testAbility, path);
                AssetDatabase.Refresh();
            }
            else
            {
                Debug.LogWarning("An asset with that name already exists.");
            }
        }


        private string GetAbilityType(AbilityBehaviour behaviour)
        {
            switch (behaviour)
            {
                case AbilityBehaviour.DRAIN:
                    return "AbilityDrain";
                case AbilityBehaviour.EXPLOSION:
                    return "AbilityExplosion";
                case AbilityBehaviour.ICARUS:
                    return "AbilityIcarus";
                case AbilityBehaviour.LUNGE:
                    return "AbilityLunge";
                case AbilityBehaviour.SPEED:
                    return "AbilitySpeed";
                case AbilityBehaviour.STRENGTH:
                    return "AbilityStrength";
                case AbilityBehaviour.TELEPORT:
                    return "AbilityTeleport";
                default:
                    return "ERROR";
            }
        }

        private void DisplayLunge()
        {
            GUIStyle headingStyle = new GUIStyle();
            headingStyle.alignment = TextAnchor.MiddleCenter;
            headingStyle.normal.textColor = Color.white;
            GUILayout.Label("Lunge settings:", headingStyle);
            EditorGUILayout.FloatField("Lunge Force", m_LungeForce);
        }

        private void DisplaySpecificSettings(AbilityBehaviour behaviour)
        {
            switch (behaviour)
            {
                case AbilityBehaviour.DRAIN:
                    break;
                case AbilityBehaviour.EXPLOSION:
                    break;
                case AbilityBehaviour.ICARUS:
                    break;
                case AbilityBehaviour.LUNGE:
                    DisplayLunge();
                    break;
                case AbilityBehaviour.SPEED:
                    break;
                case AbilityBehaviour.STRENGTH:
                    break;
                case AbilityBehaviour.TELEPORT:
                    break;
                default:
                    break;
            }
        }
	}
}
