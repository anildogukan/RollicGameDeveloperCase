using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace RollicDeveloperCase
{
    public class ElevatorGround : MonoBehaviour
    {      
        public void Move()
        {
            transform.DOLocalMoveY(-0.5f, 1.5f);
        }      

    }
}