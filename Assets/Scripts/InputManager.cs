using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    /// <summary>Delegate which handles single axis movement.</summary>
    public delegate void KeyboardMoveEvent(float x, float y);

    /// <summary>Event which handles movement.</summary>
    public static event KeyboardMoveEvent OnMove;

    /// <summary>Delegate which handles single axis rotation.</summary>
    public delegate void KeyboardLookEvent(float x, float y);

    /// <summary>Event which handles looking.</summary>
    public static event KeyboardLookEvent OnLook;

    /// <summary>Delegate which handles weapon firing.</summary>
    public delegate void KeyboardFireEvent(bool active);

    /// <summary>Event which handles weapon firing.</summary>
    public static event KeyboardFireEvent OnFire;

    /// <summary>Delegate which handles weapon switching.</summary>
    public delegate void KeyboardSwitchWeaponEvent(bool active);

    /// <summary>Event which handles weapon switching.</summary>
    public event KeyboardSwitchWeaponEvent OnSwitchWeapon;

    /// <summary>Delegate which handles in-game pausing.</summary>
    public delegate void KeyboardPauseEvent(bool active);

    /// <summary>Event which handles in-game pausing.</summary>
    public event KeyboardPauseEvent OnPause;

    public delegate void KeyboardJumpEvent(bool active);

    public static event KeyboardJumpEvent OnJump;

    public delegate void KeyboardWeaponSelect1(bool active);

    public static event KeyboardWeaponSelect1 OnSelect1;

    public delegate void KeyboardWeaponselect2(bool active);

    public static event KeyboardWeaponselect2 OnSelect2;

    public delegate void DebugToggle(bool active);

    public static event DebugToggle OnDebugToggle;

    private void Start()
    {
        //OnPause += GameManager.Instance.TogglePause;
        
    }

    private void Update()
    {
        OnPause?.Invoke(Input.GetKeyDown(KeyCode.Escape));

        OnLook?.Invoke(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        OnMove?.Invoke(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        OnFire?.Invoke(Input.GetMouseButton(0));
        OnSwitchWeapon?.Invoke(Input.GetMouseButtonDown(1));
        OnJump?.Invoke(Input.GetKeyDown(KeyCode.Space));
        OnSelect1?.Invoke(Input.GetKey(KeyCode.Alpha1));
        OnSelect2?.Invoke(Input.GetKey(KeyCode.Alpha2));
        OnDebugToggle?.Invoke(Input.GetKeyDown(KeyCode.F3));
      
        
    }
}
