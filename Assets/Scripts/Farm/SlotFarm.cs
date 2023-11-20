using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [SerializeField] private int digAmount = 3; // quantidade de vezes que o jogador deve cavar para abrir o buraco
    private int initialDigAmount;

    // Start is called before the first frame update
    void Start()
    {
        initialDigAmount = digAmount;    
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
    }

    public void OnHit()
    {
        digAmount--;

        if (digAmount <= 0)
        {
            spriteRenderer.sprite = hole;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shovel"))
            OnHit();
    }
}
