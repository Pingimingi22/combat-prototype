using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class AbilityController : MonoBehaviour
    {
        public Ability m_Ability1;
        public Ability m_Ability2;
        public Ability m_Ability3;

        public KeyCode m_Ability1Key;
        public KeyCode m_Ability2Key;
        public KeyCode m_Ability3Key;

        public Rigidbody m_Rigidbody;

        // Start is called before the first frame update
        public void Start()
        {
            if (m_Rigidbody == null)
                m_Rigidbody = GetComponent<Rigidbody>();

            Debug.Assert(m_Rigidbody); // Make sure it's set to something now.
        }

        // Update is called once per frame
        public void Update()
        {
            m_Ability1.UpdateBehaviours(m_Rigidbody);
            m_Ability2.UpdateBehaviours(m_Rigidbody);
            m_Ability3.UpdateBehaviours(m_Rigidbody);
        }
    }
}