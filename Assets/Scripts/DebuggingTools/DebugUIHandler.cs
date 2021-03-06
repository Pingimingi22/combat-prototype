using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Player;
using CustomDebug;

public class DebugUIHandler : MonoBehaviour
{

    [Header("References")]
    public PlayerController m_playerController;
    public Abilities.AbilityController m_AbilityController;
    public Canvas m_Canvas;

    [Header("Jump")]
    public Text m_isGroundedText;

    [Header("Movement")]
    public Text m_moveSpeedText;
    public Text m_isMovingText;
    public Text m_YVelocityText;
    public Text m_CacheMovDirText;
    public Text m_XAxisText;
    public Text m_ZAxisText;

    private bool m_Toggle = true;

    [Header("Weapon Sway")]
    public Text m_SwayTimerText;
    public Text m_WaveSliceText;

    [Header("Weapon General")]
    public Text m_CurrentWeaponSlotText;

    [Header("Combat General")]
    public Text m_CurrentHealthText;
    public Text m_CurrentMaxHealthText;

    public Text m_ShootHeldText;
    public Text m_ShootHeldCounterText;
    public Text m_VertRecoilText;

    [Header("Combat Abilities")]
    public Text m_Ability1ActiveText;
    public Text m_Ability1CounterText;

    public Text m_Ability2ActiveText;
    public Text m_Ability2CounterText;

    public Text m_Ability3ActiveText;
    public Text m_Ability3CounterText;

    private void Awake()
    {
        Debug.Assert(m_playerController);
        Debug.Assert(m_AbilityController);
        Debug.Assert(m_Canvas);
    }

    // Start is called before the first frame update
    void Start()
    {
        InputManager.OnDebugToggle += Toggle;
    }

    // Update is called once per frame
    void Update()
    {
        GraphicalDebugger.Assign<bool>(m_playerController.IsGrounded, "IsGrounded", m_isGroundedText);
        GraphicalDebugger.Assign<float>(m_playerController.m_currentMoveSpeed, "MoveSpeed", m_moveSpeedText);
        GraphicalDebugger.Assign<bool>(m_playerController.m_isMoving, "IsMoving", m_isMovingText);
        GraphicalDebugger.Assign<float>(m_playerController.m_WaveSlice, "WaveSlice", m_WaveSliceText);
        GraphicalDebugger.Assign<float>(m_playerController.m_SwayTimer, "SwayTimer", m_SwayTimerText);
        GraphicalDebugger.Assign<string>(PlayerManager.m_CurrentWeapon.ToString(), "CurrentWeaponSlot", m_CurrentWeaponSlotText);
        GraphicalDebugger.Assign<float>(PlayerManager.s_Health, "Health", m_CurrentHealthText);
        GraphicalDebugger.Assign<float>(PlayerManager.s_MaxHealth, "MaxHealth", m_CurrentMaxHealthText);
        GraphicalDebugger.Assign<float>(m_playerController.m_Rigidbody.velocity.y, "YVelocity", m_YVelocityText);
        GraphicalDebugger.Assign<Vector3>(m_playerController.CacheMovDir, "CacheMovDir", m_CacheMovDirText);
        GraphicalDebugger.Assign<float>(Input.GetAxis("Horizontal"), "XAxis", m_XAxisText);
        GraphicalDebugger.Assign<float>(Input.GetAxis("Vertical"), "ZAxis", m_ZAxisText);

        GraphicalDebugger.Assign<string>(m_AbilityController.m_Ability1.IsActive().ToString(), "Ability1Active", m_Ability1ActiveText);
        GraphicalDebugger.Assign<float>(m_AbilityController.m_Ability1.GetCounter(), "Ability1Counter", m_Ability1CounterText);

        GraphicalDebugger.Assign<string>(m_AbilityController.m_Ability2.IsActive().ToString(), "Ability2Active", m_Ability2ActiveText);
        GraphicalDebugger.Assign<float>(m_AbilityController.m_Ability2.GetCounter(), "Ability2Counter", m_Ability2CounterText);

        GraphicalDebugger.Assign<string>(m_AbilityController.m_Ability3.IsActive().ToString(), "Ability3Active", m_Ability3ActiveText);
        GraphicalDebugger.Assign<float>(m_AbilityController.m_Ability3.GetCounter(), "Ability3Counter", m_Ability3CounterText);

        GraphicalDebugger.Assign<bool>(m_playerController.m_HoldingFire, "ShootHeld", m_ShootHeldText);
        GraphicalDebugger.Assign<float>(m_playerController.m_HeldCounter, "ShootHeldCounter", m_ShootHeldCounterText);

        GraphicalDebugger.Assign<float>(m_playerController.m_AdditionalVerticalRecoil, "VertRecoil", m_VertRecoilText);
    }

    public void Toggle(bool active)
    {
        if (active)
        {
            if (m_Toggle)
            {
                m_Canvas.enabled = false;
                m_Toggle = false;
            }
            else
            { 
                m_Canvas.enabled = true;
                m_Toggle = true;
            }

            Debug.Log("Debug Toggled.");
        }

    }
    
}
