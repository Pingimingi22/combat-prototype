using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrototypeEditorTools;

//[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AbilityScriptableObject")]
public abstract class Ability : ScriptableObject
{
    public string m_AbilityName;
    public AbilityBehaviour m_Behaviour;
}
