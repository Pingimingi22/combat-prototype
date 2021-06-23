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
    public float m_jumpForce = 5;
    public float m_groundCheckHeight = 1;
    public float m_groundCheckRadius = 1;

    public GameObject m_weapon1;
    public GameObject m_weapon2;

    public GameObject m_hitMarker;

    [Header("Gizmos")]
    public float m_hitMarkerSize = 0.25f;

    [Header("References")]
    public Camera m_mainCamera;

    private float xRotation = 0;

    private Rigidbody m_rigidbody;

    private List<Vector3> m_hitMarkers;
    private List<GameObject> m_hitMarkersVisual;


    private float m_fireCounter = 0.0f;
    private bool m_hasFired = false;

    private bool m_isGrounded = true;



    private Vector3 m_cacheMoveDirection = Vector3.zero;
    private Vector3 m_cacheJumpThing = Vector3.zero;

    private int m_currentlySelectedWeapon = 1;

    

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


        m_isGrounded = false;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.SphereCast(ray, m_groundCheckRadius, out hit, m_groundCheckHeight))
        {
            m_isGrounded = true;
        }


        // Making sure angular velocity isn't a problem.
        m_rigidbody.angularVelocity = Vector3.zero;
    }

	private void FixedUpdate()
	{
        m_rigidbody.velocity = m_cacheMoveDirection;

        
	}

	public void Look(float xDelta, float yDelta)
    {
        xDelta *= Time.deltaTime * m_sensitivity;
        yDelta *= Time.deltaTime * m_sensitivity;


        // ---------------- Up and down camera look ---------------- //
        xRotation -= yDelta;
        xRotation = Mathf.Clamp(xRotation, -m_verticalLookLock, m_verticalLookLock);
        m_mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // ---------------- Body move left-right look ---------------- //
        transform.Rotate(Vector3.up * xDelta);
    }

	public void Move(float x, float z)
	{
        Vector3 moveDirection = transform.right * x + transform.forward * z;
        
        //moveDirection.Normalize();

        

        //m_rigidbody.velocity = (moveDirection * m_moveSpeed) + new Vector3(0, m_rigidbody.velocity.y, 0);
        m_cacheMoveDirection = (moveDirection * m_moveSpeed) + new Vector3(0, m_rigidbody.velocity.y, 0);
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
        if (active && m_isGrounded)
        {
            //m_rigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            m_cacheMoveDirection += Vector3.up * m_jumpForce;
        }
    }

	private void OnDrawGizmos()
	{


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
        }
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
}
