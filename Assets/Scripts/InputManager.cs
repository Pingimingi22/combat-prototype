using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    /// <summary>Delegate which handles single axis movement.</summary>
    public delegate void KeyboardMoveEvent(float x, float y);

    /// <summary>Event which handles movement.</summary>
    public event KeyboardMoveEvent OnMove;

    /// <summary>Delegate which handles single axis rotation.</summary>
    public delegate void KeyboardLookEvent(float x, float y);

    /// <summary>Event which handles looking.</summary>
    public static event KeyboardLookEvent OnLook;

    /// <summary>Delegate which handles weapon firing.</summary>
    public delegate void KeyboardFireEvent(bool active);

    /// <summary>Event which handles weapon firing.</summary>
    public event KeyboardFireEvent OnFire;

    /// <summary>Delegate which handles weapon switching.</summary>
    public delegate void KeyboardSwitchWeaponEvent(bool active);

    /// <summary>Event which handles weapon switching.</summary>
    public event KeyboardSwitchWeaponEvent OnSwitchWeapon;

    /// <summary>Delegate which handles in-game pausing.</summary>
    public delegate void KeyboardPauseEvent(bool active);

    /// <summary>Event which handles in-game pausing.</summary>
    public event KeyboardPauseEvent OnPause;

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
    }
}
