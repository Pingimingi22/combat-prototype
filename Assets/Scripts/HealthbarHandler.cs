using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;
public class HealthbarHandler
{
    public Image m_Image;
    public HealthbarHandler(Image image)
    {
        m_Image = image;
    }

    // Update is called once per frame
    public void Update()
    {
        m_Image.rectTransform.localScale = new Vector3((float)PlayerManager.s_Health / PlayerManager.s_MaxHealth, 1, 1);
    }
}
