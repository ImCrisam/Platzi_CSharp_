using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu, inGame, gameOver
}
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instanceGameManager;
    PlayerController controller;

    public GameState currentGameState;
    void Awake()
    {
        if (instanceGameManager == null)
        {
            instanceGameManager = this;
        }
    }
    void Start()
    {
        controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (!currentGameState.Equals(GameState.inGame))
            {
                StartGame();
            }
        }
    }


    public void StartGame()
    {
        setGameState(GameState.inGame);
        LevelManager.instance.RemoveLevelAllBlocks();
        
        Invoke("ReloadLevel", 0.3f);
    }
    public void GameOver()
    {
        setGameState(GameState.gameOver);
    }
    public void BackToMenu()
    {
        setGameState(GameState.menu);
    }

    private void setGameState(GameState state)
    {
        switch (state)
        {
            case GameState.menu:
                break;
            case GameState.inGame:
                break;
            case GameState.gameOver:
                break;
        }
        this.currentGameState = state;
    }

    void ReloadLevel()
    {
        LevelManager.instance.GenerateInitialBlocks();
        controller.StartGame();
        controller.SetAlive(true);
        
    }
}
