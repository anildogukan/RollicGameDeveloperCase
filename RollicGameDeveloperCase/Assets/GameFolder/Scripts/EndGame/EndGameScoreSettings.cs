using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScoreSettings : MonoBehaviour
{
    [SerializeField] private GroundScore[] _groundScore;
    [SerializeField] private Material[] _groundMat;


    private int score = 0;
    private int groundMatindex = 0;


    private void Start()
    {
        foreach (GroundScore scoreGround in _groundScore)
        {

            scoreGround.scoreText.text = score.ToString();
            
            score += 10;
            scoreGround.groundMesh.material = _groundMat[groundMatindex];

            if (score % 50 == 0 && groundMatindex< _groundMat.Length-1)
            {

                groundMatindex++;
            }
        }


    }
}
