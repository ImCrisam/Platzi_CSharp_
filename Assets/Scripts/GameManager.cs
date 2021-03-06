﻿using System.Collections;
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
    public GameObject player;
    public int coins;
    public float score;
    CameraFollow cameraFollow;
    PlayerController playerController;
    public GameState currentGameState = GameState.menu;
    void Awake()
    {
        //Map obj
        playerController = player.GetComponent<PlayerController>();
        cameraFollow =GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();

        //Singleton
        if (instanceGameManager == null)
        {
            instanceGameManager = this;
        }

        // deactivate player to not be seen by menu
        //player.SetActive(true);
        player.GetComponent<SpriteRenderer>().enabled = false;
        
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //Follow the player at all times if State InGame
        if(currentGameState.Equals(GameState.inGame)){
             cameraFollow.MoveCamera(playerController.IsTouchingTheGround());
        }
        
    }


    public void StartGame()
    {
       
        LevelManager.instance.RemoveLevelAllBlocks();
        
        Invoke("ReloadLevel", 0.3f);
        
        Invoke("StartTheGame", 0.4f);

        // setGameState(GameState.inGame);
        // cameraFollow.ResetCamera();

    }
    void StartTheGame(){
        setGameState(GameState.inGame);
    }
    public void GameOver()
    {
        setGameState(GameState.gameOver);
    }
    public void BackToMenu()
    {
        LevelManager.instance.RemoveLevelAllBlocks();
       
        setGameState(GameState.menu);
    }

    private void setGameState(GameState state)
    {
        switch (state)
        {
            case GameState.menu:
                MenuManager.instance.ShowMainMenu();
                break;
            case GameState.inGame:
                MenuManager.instance.ShowInGame();
                break;
            case GameState.gameOver:
                MenuManager.instance.ShowGameOver();
                break;
        }
        this.currentGameState = state;
    }

    void ReloadLevel()
    {
        coins =0;
        score =0f;
        LevelManager.instance.GenerateInitialBlocks();
        player.GetComponent<SpriteRenderer>().enabled = true;
        playerController.SetAlive(true);
        playerController.StartGame();
        cameraFollow.ResetCamera();
    }

    void ActivatePlayer(){
        //player.SetActive(true);
        player.GetComponent<SpriteRenderer>().enabled = true;

    }
    public void CollectObject(Collectable collectable){
        coins += collectable.vale;;
    }
    

}
