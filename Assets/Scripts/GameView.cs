using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public Text coinsText, scoreText;
    public int coins=0;
    public float score=0, maxScore=0;
    private string stringScore;
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instanceGameManager.currentGameState.Equals(GameState.inGame)){
            coins = GameManager.instanceGameManager.collectedObject;
            coinsText.text = coins.ToString();
            stringScore = score.ToString("f1")+"/"+maxScore.ToString("f1");
            scoreText.text = stringScore;
        }
    }
}
