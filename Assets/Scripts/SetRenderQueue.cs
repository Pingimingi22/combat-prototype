using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRenderQueue : MonoBehaviour
{
    public MeshRenderer m_renderer;
    public int renderNum = 10000;
    // Start is called before the first frame update
    void Start()
    {
        m_renderer.sharedMaterial.renderQueue = renderNum;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
