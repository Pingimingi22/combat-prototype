using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Player
{
    public enum WeaponSlot
    {
        WEAPON1,
        WEAPON2
    }


    public class PlayerManager : MonoBehaviour
    {

        public static WeaponSlot m_CurrentWeapon;
        public PlayerController m_PlayerController;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Assert(m_PlayerController, "Please give the PlayerManager a reference to the PlayerController.");
            PlayerUtilities.Init(this, m_PlayerController);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddHealth(float health)
        { }

        public void RemoveHealth(float health)
        { }

        public static void SetWeapon(WeaponSlot weapon) { m_CurrentWeapon = weapon; }
        //public void AddSpeed
    }
}