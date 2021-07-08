using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLunge : Ability
{
	public float m_LungeForce = 5;
	public bool m_HasAreaDamage = false;
	public float m_AreaDamage = 0;


	public void Invoke(Rigidbody rigidbody, float cooldown)
	{
		rigidbody.AddForce(Vector3.forward * m_LungeForce, ForceMode.Impulse);
	}
}
