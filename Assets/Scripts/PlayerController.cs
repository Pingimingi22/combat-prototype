using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float m_sensitivity = 1000.0f;
    public float m_verticalLookLock = 75.0f;
    public float m_moveSpeed = 1;
    public float m_fireRate = 0.5f;
    public float m_bulletForce = 5;
    public float m_JumpHeight = 5;
    public float m_groundCheckHeight = 1;
    public float m_groundCheckRadius = 1;
    public float m_Gravity = -9.8f;
    public float m_maxSpeed = 5;
    public float m_BobSpeed = 1;
    public float m_BobDistance = 1;

    public GameObject m_weapon1;
    public GameObject m_weapon2;

    public GameObject m_hitMarker;

    [Header("Gizmos")]
    public float m_hitMarkerSize = 0.25f;

    [Header("References")]
    public Camera m_mainCamera;
    public Transform m_orientation;

    private float xRotation = 0;

    private Rigidbody m_rigidbody;

    private List<Vector3> m_hitMarkers;
    private List<GameObject> m_hitMarkersVisual;


    private float m_fireCounter = 0.0f;
    private bool m_hasFired = false;

    public bool IsGrounded { get; private set; }



    private Vector3 m_cacheMoveDirection = Vector3.zero;
    private Vector3 m_cacheJumpThing = Vector3.zero;


    // Private multipliers to make numbers smaller.
    private float m_speedMultiplier = 1000;

    // Exposed variables for debugging.
    [HideInInspector]
    public float m_currentMoveSpeed { get; private set; }
    [HideInInspector]
    public bool m_isMoving { get; private set; }



    // Weapon sway stuff.
    [HideInInspector]
    public float m_SwayTimer = 0.0f;
    [HideInInspector]
    public float m_WaveSlice = 0.0f;
    [HideInInspector]
    public float m_WaveSliceX = 0.0f;
    [HideInInspector]
    public Vector3 m_Weapon1MidPoint;
    [HideInInspector]
    public Vector3 m_Weapon2MidPoint;


    // Start is called before the first frame update
    void Start()
    {
        InputManager.OnLook += Look;
        InputManager.OnMove += Move;
        InputManager.OnFire += Shoot;
        InputManager.OnJump += Jump;
        InputManager.OnSelect1 += WeaponSelect1;
        InputManager.OnSelect2 += WeaponSelect2;

        m_rigidbody = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;

        m_hitMarkers = new List<Vector3>();
        m_hitMarkersVisual = new List<GameObject>();


        // testing
        InputManager.OnHorizontalLook += HorizontalLook;


        // Cacheing weapon locations.
        m_Weapon1MidPoint = m_weapon1.transform.localPosition;
        m_Weapon2MidPoint = m_weapon2.transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        if (m_hasFired)
        {
            m_fireCounter += Time.deltaTime;
            if (m_fireCounter >= m_fireRate)
            {
                m_hasFired = false;
                m_fireCounter = 0;
            }
        }

        IsGrounded = CheckGrounded();

        // Making sure angular velocity isn't a problem.
        m_rigidbody.velocity = new Vector3(m_cacheMoveDirection.x, m_rigidbody.velocity.y, m_cacheMoveDirection.z);
        m_rigidbody.angularVelocity = Vector3.zero;

        m_currentMoveSpeed = m_rigidbody.velocity.magnitude;

        WeaponSway();
    }

	private void FixedUpdate()
	{

        
        
	}

	public void Look(float xDelta, float yDelta)
    {
        //xDelta *= m_sensitivity;
        //yDelta *= m_sensitivity;
        //
        //
        //// ---------------- Up and down camera look ---------------- //
        //xRotation -= yDelta;
        //xRotation = Mathf.Clamp(xRotation, -m_verticalLookLock, m_verticalLookLock);
        //m_mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //
        //// ---------------- Body move left-right look ---------------- //
        ////transform.Rotate(Vector3.up * xDelta);
        //
        //
        //m_rigidbody.rotation = m_rigidbody.rotation * Quaternion.Euler(Vector3.up * xDelta);


        float mouseX = Input.GetAxis("Mouse X") * m_sensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_sensitivity * Time.fixedDeltaTime;

        // Finding current look rotation
        Vector3 rot = m_mainCamera.transform.localRotation.eulerAngles;
        float desiredX = rot.y + mouseX;

        // Rotate
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Perform the rotations
        m_mainCamera.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        m_orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);

    }

    public void HorizontalLook(float xDelta)
    {
        //m_rigidbody.rotation *= Quaternion.Euler(Vector3.up * xDelta * 1.9f);
    }

	public void Move(float x, float z)
	{
        //m_rigidbody.AddForce(Vector3.down * Time.deltaTime * 50);


        Vector3 moveDirection = transform.right * x + transform.forward * z;
        
        Vector3 xMov = new Vector3(Input.GetAxisRaw("Horizontal") * m_orientation.right.x, 0, Input.GetAxisRaw("Horizontal") * m_orientation.right.z);
        Vector3 zMov = new Vector3(Input.GetAxisRaw("Vertical") * m_orientation.forward.x, 0, Input.GetAxisRaw("Vertical") * m_orientation.forward.z);
        
        m_cacheMoveDirection = ((xMov + zMov).normalized * m_moveSpeed * Time.deltaTime) + new Vector3(0, m_rigidbody.velocity.y, 0);


        // Tracking whether we are moving or not.
        m_isMoving = false;
        if (x != 0 || z != 0)
            m_isMoving = true; 


        // Finding velocity relative to where the player is looking
        //Vector2 mag = FindVelRelativeToLook();
        //float xMag = mag.x;
        //float yMag = mag.y;

        // Should be counter-acting bad movement?

        //if (x > 0 && xMag > m_maxSpeed)
        //    x = 0;
        //if (x < 0 && xMag < -m_maxSpeed)
        //    x = 0;
        //if (z > 0 && yMag > m_maxSpeed)
        //    z = 0;
        //if (z < 0 && yMag < -m_maxSpeed)
        //    z = 0;
        //
        //m_rigidbody.AddForce(m_orientation.transform.forward * z * m_moveSpeed * Time.deltaTime * m_speedMultiplier);
        //m_rigidbody.AddForce(m_orientation.transform.right * x * m_moveSpeed * Time.deltaTime * m_speedMultiplier);

	}

    public void Shoot(bool active)
    {
        if (active && !m_hasFired)
        { 
            Ray ray = new Ray(m_mainCamera.transform.position, m_mainCamera.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject != null)
                {
                    if (m_hitMarkers.Count > 6)
                    {
                        m_hitMarkers.RemoveAt(0);
                        Destroy(m_hitMarkersVisual[0]);
                        m_hitMarkersVisual.RemoveAt(0);
                    }


                    m_hitMarkers.Add(hit.point);
                    GameObject newMarker = GameObject.Instantiate(m_hitMarker);
                    newMarker.transform.position = hit.point;
                    newMarker.transform.LookAt(transform);
                    m_hitMarkersVisual.Add(newMarker);
                    m_hasFired = true;

                    // Adding a force to the hit object.
                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(m_mainCamera.transform.forward * m_bulletForce, ForceMode.Impulse);
                    }

                }
            }
        }
    }

    public void Jump(bool active)
    {
        if (active && IsGrounded)
        {
            //m_rigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            m_cacheMoveDirection = Vector3.up * CustomMaths.ControllerMaths.CalculateJumpForce(m_JumpHeight, m_rigidbody.mass, m_Gravity);
            m_cacheMoveDirection.x = m_rigidbody.velocity.x;
            m_cacheMoveDirection.z = m_rigidbody.velocity.z;
            m_rigidbody.velocity = m_cacheMoveDirection;
        }

        
    }

    private bool CheckGrounded()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.SphereCast(ray, m_groundCheckRadius, out hit, m_groundCheckHeight))
        {
            Debug.Log(hit.transform.name);
            return true;
        }
        return false;
    }
	private void OnDrawGizmos()
	{
        Color defaultColour = Gizmos.color;


        if (m_hitMarkers != null)
        { 
            for (int i = 0; i < m_hitMarkers.Count; i++)
            {
                Gizmos.DrawSphere(m_hitMarkers[i], m_hitMarkerSize);
            }
        }


        

        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.SphereCast(ray, m_groundCheckRadius, out hit, m_groundCheckHeight))
        {
            Gizmos.DrawLine(transform.position, hit.point);

            CustomDebug.GraphicalDebugger.DrawSphereCast(transform.position, hit.point, Color.green, m_groundCheckRadius);
        }
        else
        {
            CustomDebug.GraphicalDebugger.DrawSphereCast(transform.position, transform.position + Vector3.down, Color.red, m_groundCheckRadius);
        }



        Gizmos.color = defaultColour;
    }

    public void WeaponSelect1(bool active)
    {
        if (active)
        { 
            m_weapon1.SetActive(true);
            m_weapon2.SetActive(false);
        }
    }
    public void WeaponSelect2(bool active)
    {
        if (active)
        { 
            m_weapon1.SetActive(false);
            m_weapon2.SetActive(true);
        }
    }

    public void WeaponSway()
    {
        // Right now I'm only testing this with weapon 1.

        Vector3 localPosition = m_weapon1.transform.localPosition;
        if (m_isMoving)
        {
            // Do weapon sway stuff.
            m_SwayTimer += Time.deltaTime;
            m_WaveSlice = -(Mathf.Sin(m_SwayTimer * m_BobSpeed) + 1) / 2;
            m_WaveSliceX = Mathf.Cos(m_SwayTimer * m_BobSpeed);
            
            if (m_WaveSlice >= -0.5f)
            {
                m_WaveSlice = -1 - -(Mathf.Sin(m_SwayTimer * m_BobSpeed) + 1) / 2;
            }

            float translateChangeX = m_WaveSliceX * m_BobDistance;
            float translateChangeY = m_WaveSlice * m_BobDistance;
            localPosition.y = m_Weapon1MidPoint.y + translateChangeY;
            localPosition.x = m_Weapon1MidPoint.x + translateChangeX;

            m_weapon1.transform.localPosition = localPosition;
        }
        else
        {
            m_SwayTimer = 0.0f;
            localPosition.y = m_Weapon1MidPoint.y;
            localPosition.x = m_Weapon1MidPoint.x;
            m_weapon1.transform.localPosition = Vector3.Lerp(m_weapon1.transform.localPosition, m_Weapon1MidPoint, 0.01f);
        }
        
    }

    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = m_orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(m_rigidbody.velocity.x, m_rigidbody.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitude = m_rigidbody.velocity.magnitude;
        float yMag = magnitude * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitude * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }
}
