using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollicDeveloperCase.UI
{
    public class TapToStartScript : MonoBehaviour
    {                
        
        public void TapToStart()
        {
            GameManager.OnLevelStarted?.Invoke();
        }
    }


}