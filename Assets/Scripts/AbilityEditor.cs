using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Abilities;


namespace PrototypeEditorTools
{
    public enum AbilityType
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
        private AbilityType m_behaviour;
        private AbilityType m_Behaviour1;
        private AbilityType m_Behaviour2;
        private AbilityType m_Behaviour3;
        private AbilityType m_Behaviour4;

        private float m_Cooldown;
        // ======================================== //

        // =========== Lunge Settings =========== //
        private float m_LungeForce;
        private bool m_LungeHasAreaDamage;
        private float m_LungeAreaDamage;
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
            GUILayout.Label("Generic Settings:", EditorStyles.boldLabel);


            m_name = EditorGUILayout.TextField("Ability Name:", m_name);
            m_behaviour = (AbilityType)EditorGUILayout.EnumPopup("Ability Behaviour:", m_behaviour);
            m_Cooldown = EditorGUILayout.FloatField("Cooldown:", m_Cooldown);
            EditorGUILayout.Separator();

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
                SaveObject(testAbility);
                Debug.Log("Created a new ability.");

                AssetDatabase.CreateAsset(testAbility, path);
                AssetDatabase.Refresh();
            }
            else
            {
                Debug.LogWarning("An asset with that name already exists.");
            }
        }


        private string GetAbilityType(AbilityType behaviour)
        {
            switch (behaviour)
            {
                default: 
                   return "Ability";
            }
        }

        private void DisplayLunge()
        {
            GUIStyle headingStyle = new GUIStyle();
            headingStyle.alignment = TextAnchor.MiddleCenter;
            headingStyle.normal.textColor = Color.white;
            GUILayout.Label("Lunge settings:", headingStyle);
            EditorGUILayout.FloatField("Lunge Force", m_LungeForce);
            m_LungeHasAreaDamage = EditorGUILayout.BeginToggleGroup("Lunge Area Damage", m_LungeHasAreaDamage);
            m_LungeAreaDamage = EditorGUILayout.FloatField("Area Damage", m_LungeAreaDamage);
            EditorGUILayout.EndToggleGroup();
        }

        private void DisplaySpecificSettings(AbilityType behaviour)
        {
            switch (behaviour)
            {
                case AbilityType.DRAIN:
                    break;
                case AbilityType.EXPLOSION:
                    break;
                case AbilityType.ICARUS:
                    break;
                case AbilityType.LUNGE:
                    DisplayLunge();
                    break;
                case AbilityType.SPEED:
                    break;
                case AbilityType.STRENGTH:
                    break;
                case AbilityType.TELEPORT:
                    break;
                default:
                    break;
            }
        }

        private void SaveObject(ScriptableObject obj)
        {
            //((Ability)obj).m_AbilityName = m_name;
            //((Ability)obj).m_Behaviour0 = m_behaviour;
            //((Ability)obj).m_Cooldown = m_Cooldown;


            switch (m_behaviour)
            {
                case AbilityType.DRAIN:
                    break;
                case AbilityType.EXPLOSION:
                    break;
                case AbilityType.ICARUS:
                    break;
                case AbilityType.LUNGE:
                    //((AbilityLunge)obj).m_LungeForce = m_LungeForce;
                    //((AbilityLunge)obj).m_HasAreaDamage = m_LungeHasAreaDamage;
                    //((AbilityLunge)obj).m_AreaDamage = m_LungeAreaDamage;
                    break;
                case AbilityType.SPEED:
                    break;
                case AbilityType.STRENGTH:
                    break;
                case AbilityType.TELEPORT:
                    break;
                default:
                    break;
            }
        }
	}
}
