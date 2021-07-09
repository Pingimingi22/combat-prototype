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



        private void Drain()
        {
            // Drain behaviour.
        }

        private void Explosion()
        {
            // Explosion behaviour.
        }

        private void Lunge()
        {
            // Lunge behaviour.
        }

        private void SpeedBuff()
        {
            // Speed buff behaviour.
        }

        private void StrengthBuff()
        {
            // Strength buff behaviour.
        }
    }
}
