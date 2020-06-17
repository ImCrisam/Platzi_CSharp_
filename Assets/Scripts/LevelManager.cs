using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static LevelManager instance;

    public LevelBlock block;
    public List<LevelBlock> currentLevelBlock = new List<LevelBlock>();
    public List<LevelBlock> LevelBlocks = new List<LevelBlock>();
    public Transform openLevel;

    private void Awake() {
    if(instance == null){
        instance = this;
    }
}
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLevelBlock(){
     block= Instantiate(LevelBlocks[0]);
     Vector3 spawnPosition = Vector3.zero;
     spawnPosition = currentLevelBlock[currentLevelBlock.Count-1].endLevel.position;
     block.transform.SetParent(this.transform, false);  

     Vector3 correcion = new Vector3(
                            spawnPosition.x-block.openLevel.position.x,
                            spawnPosition.y-block.openLevel.position.y,
                            0 );
    block.transform.position = correcion;
    currentLevelBlock.Add(block);
    
    }

    public void RemoveLevelBlock(){
        block = currentLevelBlock[0];
        currentLevelBlock.Remove(block);
        Destroy(block.gameObject);
    }
    public void RemoveLevelAllBlocks(){
        while(currentLevelBlock.Count>0){
            RemoveLevelBlock();
        }
    }

    public void GenerateInitialBlocks(){
    block = Instantiate(LevelBlocks[0]);
    block.transform.position = Vector2.zero;
    block.transform.SetParent(this.transform, false);  
    currentLevelBlock.Add(block);
        for(int i=0; i<1; i++){
            AddLevelBlock();
        }
       
    }
}
