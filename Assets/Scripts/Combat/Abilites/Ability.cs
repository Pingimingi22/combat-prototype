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
        private float m_Counter = 0.0f; // For internal tracking.
        private bool m_Charging = false; // For internal tracking.

        // More internal tracking.
        private bool m_Active = false;


        private void Awake()
        {
            Debug.Log("Ability Awake() function called.");
            InitDelayedBehaviours();
            InitBehaviours();

        }
        public void UpdateBehaviours(Rigidbody rigidbody)
        {
            if (m_Active)
            {
                bool behavioursFinished = true;

                for (int i = 0; i < m_AllBehaviours.Count; i++)
                { 
                    m_AllBehaviours[i].Invoke();
                    if (!m_AllBehaviours[i].m_IsFinished)
                        behavioursFinished = false;
                }

                if (behavioursFinished)
                { 
                    m_Active = false;
                    m_Charging = true;
                }
            }
        }
        public void Invoke(Rigidbody rigidbody)
        {
            // Do all actions.
            m_Active = true;
        }
       
        private void InitDelayedBehaviours()
        {
            for (int i = 0; i < m_AllBehaviours.Count; i++)
            {
                if (m_AllBehaviours[i].m_IsDelayed)
                    m_DelayedBehaviours.Add(m_AllBehaviours[i]);
            }
        }

        private void InitBehaviours()
        {
            for (int i = 0; i < m_AllBehaviours.Count; i++)
            {
                m_AllBehaviours[i].m_AbilityRef = this;
            }
        }
    }
}