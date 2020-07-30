using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeCollec
{
    coin,
    potion
}
public class Collectable : MonoBehaviour
{
    public TypeCollec type = TypeCollec.coin;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D itemCollider;
    private bool hasBeen = false;
    public int vale = 1;
    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<CircleCollider2D>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            Collect();
        }
    }

    void Show()
    {
        spriteRenderer.enabled = true;
        itemCollider.enabled = true;
        hasBeen = false;
    }

    void Hide()
    {
        spriteRenderer.enabled = false;
        itemCollider.enabled = false;
        hasBeen = true;
    }
    void Collect()
    {
        Hide();
        switch (this.type)
        {
            case TypeCollec.coin:
                GameManager.instanceGameManager.CollectObject(this);
                break;
            case TypeCollec.potion:

                break;
            default:

                break;
        }

    }
}
