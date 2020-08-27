using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public Text coinsText, scoreText;
    public int coins = 0;
    public float score, maxScore = 0, multiCoins = 5.2f;
    private string stringScore;
    // Start is called before the first frame update
    private PlayerController controller;
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instanceGameManager.currentGameState.Equals(GameState.inGame))
        {
            coins = GameManager.instanceGameManager.collectedObject;
            coinsText.text = coins.ToString();
            score = controller.GetTravelDistrance() + coins * multiCoins;
            maxScore = PlayerPrefs.GetFloat("maxScorre", 0);
            stringScore = maxScore==0 
                        ? score.ToString("f1") : score.ToString("f1")+"/"+maxScore.ToString("f1");
            
            scoreText.text = stringScore;
        }
    }
}
