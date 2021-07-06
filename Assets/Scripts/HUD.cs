using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour
{
    public Image m_HealthbarImage;

    private HealthbarHandler m_HealthHandler;

    // Start is called before the first frame update
    void Start()
    {
        m_HealthHandler = new HealthbarHandler(m_HealthbarImage);
    }

    // Update is called once per frame
    void Update()
    {
        m_HealthHandler.Update();
    }
}
