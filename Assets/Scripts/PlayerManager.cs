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

        // Temporary health variable until I get the possession system.
        public static int s_Health = 100;
        public static int s_MaxHealth = 100;


        // Start is called before the first frame update
        void Start()
        {
            Debug.Assert(m_PlayerController, "Please give the PlayerManager a reference to the PlayerController.");
            PlayerUtilities.Init(this, m_PlayerController);
        }

        // Update is called once per frame
        void Update()
        {
            Debugging.DebugCommands.Update();
        }

        public static void AddHealth(int health)
        {
            s_Health += health;
        }

        public static void RemoveHealth(int health)
        {
            s_Health -= health;
        }

        public static void SetWeapon(WeaponSlot weapon) { m_CurrentWeapon = weapon; }
        //public void AddSpeed
    }
}