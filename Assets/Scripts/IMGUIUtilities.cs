using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIUtilities
{
    public class IMGUIUtilities
    {
        public static GUIStyle m_Header;
        public static GUIStyle m_Subheader;

        public static void Init()
        {
            m_Header = new GUIStyle();
            m_Subheader = new GUIStyle();

            m_Header.fontSize = 35;
            m_Header.alignment = TextAnchor.UpperCenter;
            m_Header.fontStyle = FontStyle.Bold;
            m_Header.normal.textColor = Color.white;


            m_Subheader.fontSize = 20;
            m_Subheader.alignment = TextAnchor.MiddleCenter;
            m_Subheader.normal.textColor = Color.white;
        }

        
    }
}