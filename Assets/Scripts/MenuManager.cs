using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Canvas canvasMenu, canvasGameOver, canvasPlay;
    public static MenuManager instance;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMainMenu( ){
        canvasMenu.enabled =true;
        canvasGameOver.enabled = false;
        canvasPlay.enabled = false;
    }
     public void ShowGameOver( ){
        canvasGameOver.GetComponent<GameOver>().cargar();
        canvasMenu.enabled =false;
        canvasGameOver.enabled = true;
        canvasPlay.enabled = false;
     }
     public void ShowInGame( ){
        canvasMenu.enabled =false;
        canvasGameOver.enabled = false;
        canvasPlay.enabled = true;
     }
    public void ExitGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying= false;
        #else
        Application.Quit();
        #endif
    }
}
