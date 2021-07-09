using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrototypeEditorTools;
using System;

namespace Abilities
{
[Serializable]
    public class AbilityBehaviour
    {
        public AbilityType m_Type;
        public float m_Duration;
        public bool m_IsDelayed;
        public float m_StartTime;

        public Ability m_AbilityRef;

        public bool m_Active = false;
        public bool m_IsFinished = false;

        private float m_Counter = 0.0f; // For internal tracking.

        public void Invoke()
        {
            m_Active = true;
            if (m_Active)
                m_Counter += Time.deltaTime;
            else
                m_Counter = 0.0f;

            if ((m_Active && !m_IsDelayed) || (m_Active && m_Counter >= m_StartTime))
            { 
                switch (m_Type)
                {
                    case AbilityType.DRAIN:
                        Drain();
                        break;
                    case AbilityType.EXPLOSION:
                        Explosion();
                        break;
                    case AbilityType.LUNGE:
                        Lunge();
                        break;
                    case AbilityType.SPEED:
                        SpeedBuff();
                        break;
                    case AbilityType.STRENGTH:
                        StrengthBuff();
                        break;
                }
            }
        }

        private void Drain()
        {
            // Drain behaviour.
            if (m_Counter >= m_Duration)
            {
                m_Active = false;
                OnDrainEnd();
                return; // Early out.
            }

            // Otherwise, do drain stuff here.

        }
        private void OnDrainEnd()
        { 
            // Drain end behaviour.
        }

        private void Explosion()
        {
            // Explosion behaviour.
            // Drain behaviour.
            if (m_Counter >= m_Duration)
            {
                m_Active = false;
                OnExplosionEnd();
                return; // Early out.
            }
        }
        private void OnExplosionEnd()
        { 
            // Explosion end behaviour.
        }

        private void Lunge()
        {
            // Lunge behaviour.
            // Drain behaviour.
            if (m_Counter >= m_Duration)
            {
                m_Active = false;
                OnLungeEnd();
                return; // Early out.
            }
        }
        private void OnLungeEnd()
        { 
            // Lunge end behaviour.
        }

        private void SpeedBuff()
        {
            // Speed buff behaviour.
            // Drain behaviour.
            if (m_Counter >= m_Duration)
            {
                m_Active = false;
                OnSpeedBuffEnd();
                return; // Early out.
            }
        }
        private void OnSpeedBuffEnd()
        { 
            // Speed buff end behaviour.
        }

        private void StrengthBuff()
        {
            // Strength buff behaviour.
            // Drain behaviour.
            if (m_Counter >= m_Duration)
            {
                m_Active = false;
                OnStrengthBuffEnd();
                return; // Early out.
            }
        }
        private void OnStrengthBuffEnd()
        { 
            // Strength buff end behaviour.
        }
    }
}
