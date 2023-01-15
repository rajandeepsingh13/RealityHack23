using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

    /// <summary>
    /// 
    /// </summary>
public class Score : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreboardLabel;
    
    public void ChangeScoreBy(int scoreChange) {
        score += scoreChange;
        scoreboardLabel.text = score.ToString();
    }
}
