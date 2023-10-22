using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    public TMP_Text highscores;

    private void Start()
    {
        float[] scores = SaveData.loadScores();
        highscores.text =
            "#1: " + (scores[0] == -999 ? "" : (int)scores[0]) + "\n" +
            "#2: " + (scores[1] == -999 ? "" : (int)scores[1]) + "\n" +
            "#3: " + (scores[2] == -999 ? "" : (int)scores[2]) + "\n" +
            "#4: " + (scores[3] == -999 ? "" : (int)scores[3]) + "\n" +
            "#5: " + (scores[4] == -999 ? "" : (int)scores[4]);
    }
}
