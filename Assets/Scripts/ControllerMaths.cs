﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CustomMaths
{ 
    public class ControllerMaths
    {
        public static float CalculateJumpForce(float wantedHeight, float weight, float g)
        {
            float jumpForceRequired = Mathf.Sqrt(-2.0f * Physics.gravity.y * wantedHeight);

            return jumpForceRequired;
            
        }
    }
}
