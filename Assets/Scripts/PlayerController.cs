using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Combat;
using CombatUtilities;

namespace Player
{
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

        [Header("Weapons")]
        public Weapon m_weapon1;
        public Weapon m_weapon2;

        [Header("Other References")]
        public PlayerManager m_PlayerManager;
        public Camera m_mainCamera;
        public Transform m_orientation;

        // Private references.
        public Rigidbody Rigidbody { get; private set; }

        private float xRotation = 0;


        // Public bookkeeping.
        public bool IsGrounded { get; private set; }
        private Vector3 m_cacheMoveDirection = Vector3.zero;
   
        // Private bookkeeping.
        private float m_fireCounter = 0.0f;
        private bool m_hasFired = false;


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


        // Start is called before the first frame update
        void Start()
        {
            Debug.Assert(m_PlayerManager);

            InputManager.OnLook += Look;
            InputManager.OnMove += Move;
            InputManager.OnFire += Shoot;
            InputManager.OnJump += Jump;
            InputManager.OnSelect1 += WeaponSelect1;
            InputManager.OnSelect2 += WeaponSelect2;

            Rigidbody = GetComponent<Rigidbody>();

            Cursor.lockState = CursorLockMode.Locked;

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
            Rigidbody.velocity = new Vector3(m_cacheMoveDirection.x, Rigidbody.velocity.y, m_cacheMoveDirection.z);
            Rigidbody.angularVelocity = Vector3.zero;

            m_currentMoveSpeed = Rigidbody.velocity.magnitude;

            WeaponBob();
        }


        public void Look(float xDelta, float yDelta)
        {
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

        public void Move(float x, float z)
        {
            m_isMoving = false;
            if (x != 0 || z != 0)
                m_isMoving = true;
            else
            {


               Debug.Log("move direction:" + m_cacheMoveDirection);

                //m_cacheMoveDirection *= 0.97f;
               
               //m_cacheMoveDirection.x = Mathf.Clamp(m_cacheMoveDirection.x, 0, 15);
               //m_cacheMoveDirection.z = Mathf.Clamp(m_cacheMoveDirection.z, 0, 15);
            }


            if (!IsGrounded)
            {
                // Slightly weaker movement.
                Vector3 currentVel = m_cacheMoveDirection;
                Vector3 desiredVel = CalcuateMoveDirection(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), m_moveSpeed);

                Vector3 requiredChange = desiredVel - currentVel;

                m_cacheMoveDirection += requiredChange * 0.1f;

            }
            else
            {
                // Full on movement.
                /*
                 * 
                 * 
                
                ///The following code will do linear ramp up/down
                Vec2 currentVel;
                Vec2 desiredVel;    //Determined directly by input

                Vec2 requiredChange = desiredVel - currentVel;
                float maxChangePerFrame;
                if (requiredChange.Magnitude() > maxChangePerFrame)
                {
                    requiredChange = requiredChange.Normalized() * maxChangePerFrame;
                }
                currentVel += requiredChange;

                
                ///The following code will do exponential decay for ramp up/down
                Vec2 currentVel;
                Vec2 desiredVel;    //Determined directly by input

                Vec2 requiredChange = desiredVel - currentVel;
                
                currentVel += requiredChange * 0.3f;    //Make this fraction 1.0 for instant acceleration, or approach 0 to make it very gradual

                 * */

                //m_cacheMoveDirection += CalcuateMoveDirection(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), m_moveSpeed);
                Vector3 currentVel = m_cacheMoveDirection;
                Vector3 desiredVel = CalcuateMoveDirection(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), m_moveSpeed);

                Vector3 requiredChange = desiredVel - currentVel;

                m_cacheMoveDirection += requiredChange * 0.3f;

               
            }

            if (m_cacheMoveDirection.magnitude > 15)
            {
                m_cacheMoveDirection = m_cacheMoveDirection.normalized * 15;
                //Debug.Log("============== Surpassed max move speed: " + m_cacheMoveDirection);
            }

        }

        private Vector3 CalcuateMoveDirection(float x, float z, float speedMultiplier)
        {
            Vector3 moveDir = new Vector3();

            moveDir = transform.right * x + transform.forward * z;

            Vector3 xMov = new Vector3(x * m_orientation.right.x, 0, x * m_orientation.right.z);
            Vector3 zMov = new Vector3(z * m_orientation.forward.x, 0, z * m_orientation.forward.z);

            moveDir = ((xMov + zMov).normalized * speedMultiplier * Time.deltaTime) + new Vector3(0, Rigidbody.velocity.y, 0);

            return moveDir;
        }

        public void Shoot(bool active)
        {
            if (active && !m_hasFired)
            {
                Ray ray = new Ray(m_mainCamera.transform.position, m_mainCamera.transform.forward);
                RaycastHit hit;
                Weapon currentWeapon = PlayerUtilities.GetCurrentWeapon();
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject != null)
                    {
                        Decal newDecal = new Decal(hit.transform, hit.point, currentWeapon.m_HitDecal, hit.normal);
                        GameManager.Instance.AddDecal(newDecal);

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
                m_cacheMoveDirection = Vector3.up * CustomMaths.ControllerMaths.CalculateJumpForce(m_JumpHeight, Rigidbody.mass, m_Gravity);
                m_cacheMoveDirection.x = Rigidbody.velocity.x;
                m_cacheMoveDirection.z = Rigidbody.velocity.z;
                Rigidbody.velocity = m_cacheMoveDirection;
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

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(-m_cacheMoveDirection.x, m_cacheMoveDirection.y, -m_cacheMoveDirection.z));

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(m_cacheMoveDirection.x, m_cacheMoveDirection.y, m_cacheMoveDirection.z));

        }

        public void WeaponSelect1(bool active)
        {
            if (active)
            {
                m_weapon1.gameObject.SetActive(true);
                m_weapon2.gameObject.SetActive(false);
                PlayerManager.SetWeapon(Player.WeaponSlot.WEAPON1);
            }
        }
        public void WeaponSelect2(bool active)
        {
            if (active)
            {
                m_weapon1.gameObject.SetActive(false);
                m_weapon2.gameObject.SetActive(true);
                PlayerManager.SetWeapon(WeaponSlot.WEAPON2);
            }
        }

        public void WeaponBob()
        {
            Weapon currentWeapon = PlayerUtilities.GetCurrentWeapon();
            Vector3 localPosition = currentWeapon.transform.localPosition;
            Vector3 currentWeaponMidPoint = currentWeapon.m_MidPoint;

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
                localPosition.y = currentWeaponMidPoint.y + translateChangeY;
                localPosition.x = currentWeaponMidPoint.x + translateChangeX;

                currentWeapon.transform.localPosition = localPosition;
            }
            else
            {
                m_SwayTimer = 0.0f;
                localPosition.y = currentWeaponMidPoint.y;
                localPosition.x = currentWeaponMidPoint.x;
                currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, currentWeapon.m_MidPoint, 0.01f);
            }

        }

        public void WeaponSway()
        { 
            
        }

        public Vector2 FindVelRelativeToLook()
        {
            float lookAngle = m_orientation.transform.eulerAngles.y;
            float moveAngle = Mathf.Atan2(Rigidbody.velocity.x, Rigidbody.velocity.z) * Mathf.Rad2Deg;

            float u = Mathf.DeltaAngle(lookAngle, moveAngle);
            float v = 90 - u;

            float magnitude = Rigidbody.velocity.magnitude;
            float yMag = magnitude * Mathf.Cos(u * Mathf.Deg2Rad);
            float xMag = magnitude * Mathf.Cos(v * Mathf.Deg2Rad);

            return new Vector2(xMag, yMag);
        }

        
    }
}