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
    public Canvas m_Canvas;

    [Header("Jump")]
    public Text m_isGroundedText;

    [Header("Movement")]
    public Text m_moveSpeedText;
    public Text m_isMovingText;
    public Text m_YVelocityText;
    
    private bool m_Toggle = true;

    [Header("Weapon Sway")]
    public Text m_SwayTimerText;
    public Text m_WaveSliceText;

    [Header("Weapon General")]
    public Text m_CurrentWeaponSlotText;

    [Header("Combat General")]
    public Text m_CurrentHealthText;
    public Text m_CurrentMaxHealthText;

    private void Awake()
    {
        Debug.Assert(m_playerController);
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
        GraphicalDebugger.Assign<float>(m_playerController.Rigidbody.velocity.y, "YVelocity", m_YVelocityText);
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
