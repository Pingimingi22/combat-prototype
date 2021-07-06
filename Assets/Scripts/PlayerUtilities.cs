using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Player
{ 
	public class PlayerUtilities
	{
		public static PlayerManager s_PlayerManager;
		public static PlayerController s_PlayerController;

		public static void Init(PlayerManager manager, PlayerController controller)
		{
			s_PlayerManager = manager;
			s_PlayerController = controller;

		}
		public static Weapon GetCurrentWeapon()
		{
			Debug.Assert(s_PlayerController, "PlayerController is a null reference.");
			Debug.Assert(s_PlayerManager, "PlayerManager is a null reference.");

			switch (PlayerManager.m_CurrentWeapon)
			{
				case WeaponSlot.WEAPON1:
					return s_PlayerController.m_weapon1;
				case WeaponSlot.WEAPON2:
					return s_PlayerController.m_weapon2;
				default:
					return s_PlayerController.m_weapon1;
			}
		}
		
	}
}
