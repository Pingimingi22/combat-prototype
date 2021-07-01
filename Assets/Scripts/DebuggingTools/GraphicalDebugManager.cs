using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CustomDebug
{ 
    public class GraphicalDebugManager : MonoBehaviour
    {
        [Tooltip("Make sure you drag in a gameobject that's within the scene.")]
        public Canvas m_debugCanvasPrefab;
        [Tooltip("Make sure you drag in a gameobject that's within the scene.")]
        public Text m_textPrefab; // Just storing this here so we can instantiate text from GraphicalDebugger.

        public float m_borderMarginX = 0.5f;
        public float m_borderMarginY = 0.5f;

        private void Awake()
        {
            // Initialising the static GraphicalDebugger class.
            GraphicalDebugger.m_debugManager = this;
            GraphicalDebugger.Init();
        }
    
        void Start()
        {
            
        }
        void Update()
        {
            
        }
    
        void Toggle(bool toggle)
        {
            if (toggle)
                m_debugCanvasPrefab.gameObject.SetActive(true);
            else
                m_debugCanvasPrefab.gameObject.SetActive(false);
        }
    }
}
