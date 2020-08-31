using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text scoreText, coinsText;
    public int coins;
    public float score, maxScore;
    string stringScore;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void cargar()
    {
        coins = GameManager.instanceGameManager.coins;
        score = GameManager.instanceGameManager.score;
        maxScore = PlayerPrefs.GetFloat("maxScorre", 0);
        stringScore = maxScore == 0 || score==maxScore
                    ? score.ToString("f1") : score.ToString("f1") + "/" + maxScore.ToString("f1");

        scoreText.text = stringScore;
        coinsText.text = coins.ToString();
    }
}
