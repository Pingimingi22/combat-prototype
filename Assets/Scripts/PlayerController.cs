using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float m_sensitivity = 1000.0f;

    public float m_verticalLookLock = 75.0f;

    public Camera m_mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.OnLook += Look;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Look(float xDelta, float yDelta)
    {
        xDelta *= Time.deltaTime * m_sensitivity;
        yDelta *= Time.deltaTime * m_sensitivity;
    
        var euler = m_mainCamera.transform.rotation.eulerAngles;
        euler.x -= yDelta;
        euler.y += xDelta;
        euler.x = Mathf.Clamp(euler.x, -75, m_verticalLookLock);
        
        m_mainCamera.transform.eulerAngles = euler;

        //if (m_mainCamera.transform.rotation.eulerAngles.x > m_verticalLookLock)
        //{
        //    // Lock it back.
        //    Vector3 lockedRotation = m_mainCamera.transform.rotation.eulerAngles;
        //    lockedRotation.x = m_verticalLookLock;
        //    m_mainCamera.transform.rotation = Quaternion.Euler(lockedRotation.x, lockedRotation.y, lockedRotation.z);
        //    Debug.Log("Greater than lock.");
        //}
        //else if (m_mainCamera.transform.rotation.eulerAngles.x < -m_verticalLookLock)
        //{
        //    // Lock it back.
        //    Vector3 lockedRotation = m_mainCamera.transform.rotation.eulerAngles;
        //    lockedRotation.x = -m_verticalLookLock;
        //    m_mainCamera.transform.rotation = Quaternion.Euler(lockedRotation.x, lockedRotation.y, lockedRotation.z);
        //    Debug.Log("Less than lock.");
        //}
    }
}
