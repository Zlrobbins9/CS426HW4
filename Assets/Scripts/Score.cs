using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    public int score = 0;

    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score;
    }

    public void AddPoint()
    {
        score++;
        Debug.Log("Score: " + score);
    }
}

