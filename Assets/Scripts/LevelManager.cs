using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tier
{
    Init, Easy, Medium, Hard
}
public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static LevelManager instance;

    LevelBlock block;
    public LevelBlock blockInit;
    public List<LevelBlock> currentLevelBlock = new List<LevelBlock>();
    public List<LevelBlock> LevelBlocksEasy = new List<LevelBlock>();
    public List<LevelBlock> LevelBlocksMedium = new List<LevelBlock>();
    public List<LevelBlock> LevelBlocksHard = new List<LevelBlock>();
    public Transform openLevel;
    private int cycle, countCycle;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        countCycle = 0;
    }
    void Start()
    {
        cycle = LevelBlocksEasy.Count - 1 + LevelBlocksMedium.Count - 2 + LevelBlocksHard.Count - 1 - 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddLevelBlock()
    {
        block = Instantiate(GenerateBlocks());
        Vector3 spawnPosition = Vector3.zero;
        spawnPosition = currentLevelBlock[currentLevelBlock.Count - 1].endLevel.position;
        block.transform.SetParent(this.transform, false);

        Vector3 correcion = new Vector3(
                               spawnPosition.x - block.openLevel.position.x,
                               spawnPosition.y - block.openLevel.position.y,
                               0);
        block.transform.position = correcion;
        currentLevelBlock.Add(block);

    }

    public void RemoveLevelBlock()
    {
        block = currentLevelBlock[0];
        currentLevelBlock.Remove(block);
        Destroy(block.gameObject);
    }
    public void RemoveLevelAllBlocks()
    {
        while (currentLevelBlock.Count > 0)
        {
            RemoveLevelBlock();
        }
    }

    public void GenerateInitialBlocks()
    {
        block = Instantiate(blockInit);
        block.transform.position = Vector2.zero;
        block.transform.SetParent(this.transform, false);
        currentLevelBlock.Add(block);
        countCycle=0;
        AddLevelBlock();

        
    }
    private LevelBlock GenerateBlocks()
    {
        countCycle = countCycle > cycle ? 0 : countCycle;

        if (countCycle < LevelBlocksEasy.Count - 1)
        {
            block = BlocksRandom(Tier.Easy);
        }
        else if (countCycle < LevelBlocksMedium.Count - 2 + LevelBlocksEasy.Count - 1)
        {
            block = BlocksRandom(Tier.Medium);
        }
        else
        {
            block = BlocksRandom(Tier.Hard);

        }
        countCycle++;
        return block;
    }

    private LevelBlock BlocksRandom(Tier tier)
    {
        switch (tier)
        {
            case Tier.Easy:
                block = LevelBlocksEasy[Random.Range(0, LevelBlocksEasy.Count - 1)];
                break;
            case Tier.Medium:
                block = LevelBlocksMedium[Random.Range(0, LevelBlocksMedium.Count - 1)];
                break;
            case Tier.Hard:
                block = LevelBlocksHard[Random.Range(0, LevelBlocksHard.Count - 1)];
                break;
            default:
                block = blockInit;
                break;
        }
        return block != null ? block : BlocksRandom(tier - 1);
    }
}
