using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatUtilities
{
    public class Decal
    {
        public GameObject m_Decal;
        public Vector3 m_hitPoint;
        private Transform m_objAttached;

        public Decal(Transform obj, Vector3 hitPoint, GameObject hitDecal)
        {
            m_Decal = GameObject.Instantiate<GameObject>(hitDecal);
            m_objAttached = obj;
            m_hitPoint = m_objAttached.InverseTransformPoint(hitPoint);
        }

        public void Update()
        {
            Matrix4x4 attachedObjMat = m_objAttached.worldToLocalMatrix;
            Vector3 pos = m_hitPoint;
            Vector3 localPos = attachedObjMat * pos;
            m_Decal.transform.position = m_objAttached.TransformPoint(m_hitPoint);
        }

        public void DrawGizmo()
        {
            Gizmos.DrawSphere(m_Decal.transform.position, 0.5f);
        }

    }
}
