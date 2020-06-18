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
    public Transform StartPlayer;
    
    public GameObject player;
    CameraFollow cameraFollow;
    public GameState currentGameState = GameState.menu;
    void Awake()
    {

        player = Instantiate(player);
        cameraFollow =GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
        cameraFollow.SetTraget(player.transform);
        if (instanceGameManager == null)
        {
            instanceGameManager = this;
        }
    }
    void Start()
    {
        
        player.SetActive(false);
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
         if(currentGameState.Equals(GameState.inGame)){
             cameraFollow.MoveCamera(true);
        }
        
    }


    public void StartGame()
    {
        setGameState(GameState.inGame);
        LevelManager.instance.RemoveLevelAllBlocks();
        player.SetActive(true);
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
        PlayerController controller = player.GetComponent<PlayerController>();
        LevelManager.instance.GenerateInitialBlocks();
        controller.StartGame();
        controller.SetAlive(true);
    }
}
