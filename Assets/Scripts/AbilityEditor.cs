using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Abilities;
using UIUtilities;

namespace PrototypeEditorTools
{
    public enum AbilityType
    { 
        LUNGE,
        EXPLOSION,
        SPEED,
        STRENGTH,
        DRAIN,
        NULL
    }
    public class AbilityEditor : EditorWindow
    {
        // =========== Generic Settings =========== //
        private string m_name = "Default Ablitity";
        private AbilityBehaviour m_Behaviour0 = new AbilityBehaviour();
        private AbilityBehaviour m_Behaviour1 = new AbilityBehaviour();
        private AbilityBehaviour m_Behaviour2 = new AbilityBehaviour();
        private AbilityBehaviour m_Behaviour3 = new AbilityBehaviour();
        private AbilityBehaviour m_Behaviour4 = new AbilityBehaviour();

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
            window.minSize = new Vector2(500, 800);
            window.maxSize = new Vector2(500, 800);

            IMGUIUtilities.Init();
        }

        private void OnGUI()
        {
            GUILayout.Label("Ability Creator", IMGUIUtilities.m_Header);
            GUILayout.Label("Generic Settings:", EditorStyles.boldLabel);


            m_name = EditorGUILayout.TextField("Ability Name:", m_name);
            m_Cooldown = EditorGUILayout.FloatField("Cooldown:", m_Cooldown);
            EditorGUILayout.Separator();
            EditorGUILayout.Space(25);
            GUILayout.Label("Behaviours", IMGUIUtilities.m_Subheader);

            DisplayGenericBehaviour(m_Behaviour0);
            DisplayGenericBehaviour(m_Behaviour1);
            DisplayGenericBehaviour(m_Behaviour2);
            DisplayGenericBehaviour(m_Behaviour3);
            DisplayGenericBehaviour(m_Behaviour4);

            if (GUILayout.Button("Create Ability"))
            {
                string path = "Assets/" + m_name + ".asset";

                CreateAbility(m_name, path);
            }
        }

        private void CreateAbility(string name, string path)
        {
            ScriptableObject testAbility = ScriptableObject.CreateInstance("Ability");
    
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
                case AbilityType.LUNGE:
                    DisplayLunge();
                    break;
                case AbilityType.SPEED:
                    break;
                case AbilityType.STRENGTH:
                    break;
                default:
                    break;
            }
        }

        private void SaveObject(ScriptableObject obj)
        {
           // ((Ability)obj).m_AbilityName = m_name;
           // ((Ability)obj).m_Behaviour0 = m_behaviour;
           // ((Ability)obj).m_Cooldown = m_Cooldown;
           //
           //
           // switch (m_behaviour)
           // {
           //     case AbilityType.DRAIN:
           //         break;
           //     case AbilityType.EXPLOSION:
           //         break;
           //     case AbilityType.LUNGE:
           //         //((AbilityLunge)obj).m_LungeForce = m_LungeForce;
           //         //((AbilityLunge)obj).m_HasAreaDamage = m_LungeHasAreaDamage;
           //         //((AbilityLunge)obj).m_AreaDamage = m_LungeAreaDamage;
           //         break;
           //     case AbilityType.SPEED:
           //         break;
           //     case AbilityType.STRENGTH:
           //         break;
           //     default:
           //         break;
           // }
        }

        private void DisplayGenericBehaviour(AbilityBehaviour behaviour)
        {
            behaviour.m_Type = (AbilityType)EditorGUILayout.EnumPopup("Ability Type", behaviour.m_Type);
            behaviour.m_Duration = EditorGUILayout.FloatField("Duration", behaviour.m_Duration);
            behaviour.m_IsDelayed = EditorGUILayout.BeginToggleGroup("Delayed Cast", behaviour.m_IsDelayed);
            behaviour.m_StartTime = EditorGUILayout.FloatField("Behaviour Start Time", behaviour.m_StartTime);
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(25);
        }
	}
}
