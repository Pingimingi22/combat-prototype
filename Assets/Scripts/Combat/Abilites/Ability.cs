using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrototypeEditorTools;


namespace Abilities
{
    //[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AbilityScriptableObject")]
    public class Ability : ScriptableObject
    {
        public string m_AbilityName;

        public List<AbilityBehaviour> m_AllBehaviours = new List<AbilityBehaviour>();

        private List<AbilityBehaviour> m_DelayedBehaviours = new List<AbilityBehaviour>(); // Internally tracks all of the behaviours marked as delayed.
                                                                                           // This is so I can loop through them all and invoke them at the right time.

        public float m_Cooldown;
        private float m_Counter; // For internal tracking.

        [Tooltip("If checked yes, this ability is supposed to be used once and instantly, otherwise its a long lasting ability.")]
        public bool m_InstantAction;

        // More internal tracking.
        private bool m_Active = false;


        private void Awake()
        {
            Debug.Log("Ability Awake() function called.");
            InitDelayedBehaviours();
        }
        public void Update()
        {
            if (m_Active)
            {
                InvokeLongLasting();
            }
        }
        public void Invoke()
        {
            // Do all actions.
            m_Active = true;
        }

        private void InvokeLongLasting()
        {
            // Will keep invoking the behaviours that require multiple frames.
            for (int i = 0; i < m_DelayedBehaviours.Count; i++)
            { 
                
            }
        }

        private void InvokeDelayedBehaviour()
        { }

       
        private void InitDelayedBehaviours()
        {
            for (int i = 0; i < m_AllBehaviours.Count; i++)
            {
                if (m_AllBehaviours[i].m_IsDelayed)
                    m_DelayedBehaviours.Add(m_AllBehaviours[i]);
            }
        }
    }
}