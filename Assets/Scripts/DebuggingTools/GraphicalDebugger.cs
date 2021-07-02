using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CustomDebug
{ 
    public class GraphicalDebugger
    {

        public static GraphicalDebugManager m_debugManager;

        // Container which stores all created text elements.
        public static List<Text> m_createdText = new List<Text>();



        private static RectTransform m_canvasRect;
        private static int m_maxTextInColoumn;

        private static int m_coloumnCounter = 0;
        private static int m_usedColoumns = 0;


        public static void Init()
        { 
            m_canvasRect = m_debugManager.m_debugCanvasPrefab.GetComponent<RectTransform>();
            m_maxTextInColoumn = (int)(m_canvasRect.rect.height / (m_debugManager.m_textPrefab.rectTransform.rect.height + m_debugManager.m_borderMarginY));
            Debug.Log(m_maxTextInColoumn);
        }


        /// <summary>
        /// Creates a text element which will be displayed on the debug tools canvas.
        /// </summary>
        /// <param name="display">string to display</param>
        /// <param name="fontSize">size of font</param>
        public static void CreateText(string display, float fontSize)
        {
            Text newText = GameObject.Instantiate(m_debugManager.m_textPrefab);

            RectTransform canvas = m_debugManager.m_debugCanvasPrefab.GetComponent<RectTransform>();

            // Getting the positions.
            float textXPos = 0;
            float textYPos = canvas.rect.height;

            newText.transform.SetParent(m_debugManager.m_debugCanvasPrefab.transform);

            // Setting anchor point.
            newText.alignment = TextAnchor.MiddleLeft;

            if (m_coloumnCounter > m_maxTextInColoumn)
            { 
                m_usedColoumns++;
                m_coloumnCounter = 0;
            }

            newText.transform.position = new Vector3(textXPos + m_debugManager.m_borderMarginX + (m_usedColoumns * 100), textYPos - m_debugManager.m_borderMarginY - (m_coloumnCounter * 50), 0);

            m_createdText.Add(newText);
            m_coloumnCounter++;

            // Now we have to set the position of the text. The way I have in plan should just place the text in the next available "slot".

        }

        
        /// <summary>
        /// Assigns variable to be displayed for a text element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varToDisplay">Variable you want to display.</param>
        /// <param name="prefixText">Text to display before the variable.</param>">
        /// <param name="textObj">The text element.</param>
        public static void Assign<T>(T varToDisplay, string prefixText, Text textObj)
        {
            textObj.text = prefixText + ": " + varToDisplay.ToString();
        }


        /// <summary>
        /// Draws sphere casts.
        /// </summary>
        /// <param name="start">Start point.</param>
        /// <param name="end">End point.</param>
        public static void DrawSphereCast(Vector3 start, Vector3 end, Color colour, float radius)
        {
            Color defaultColour = Gizmos.color;
            Gizmos.color = colour;

            

            for (int i = 0; i < 5; i++)
            {
                Vector3 circlePoint = Vector3.Lerp(start, end, (float)i / 5);
                Gizmos.DrawWireSphere(circlePoint, radius);
            }

            Gizmos.color = defaultColour;
        }
    }
}
