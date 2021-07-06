using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class Weapon : MonoBehaviour
    {

        public Vector3 m_MidPoint { get; private set; }



        // Start is called before the first frame update
        void Start()
        {
            m_MidPoint = transform.localPosition;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}