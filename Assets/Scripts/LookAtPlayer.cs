using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform m_player;
    // Start is called before the first frame update
    void Start()
    {
        if (m_player == null)
            m_player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(m_player);
    }
}
