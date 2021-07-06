using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;


namespace Debugging
{
    public class DebugCommands
    {
        public static void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
                PlayerManager.RemoveHealth(5);
            if (Input.GetKeyDown(KeyCode.H))
                PlayerManager.AddHealth(5);
        }
    }
}