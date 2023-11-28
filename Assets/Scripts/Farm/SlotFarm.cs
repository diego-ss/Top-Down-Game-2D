using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip holeSFX;
    [SerializeField] private AudioClip carrotSFX;

    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;
    private PlayerItems playerItems;

    [Header("Settings")]
    [SerializeField] private bool watering;
    [SerializeField] private bool isHole;
    [SerializeField] private bool playerCanCollect;
    [SerializeField] private bool isCarrot;
    [SerializeField] private int digAmount = 3; // quantidade de vezes que o jogador deve cavar para abrir o buraco
    private int initialDigAmount;
    [SerializeField] private float waterAmount; // quantidade de água que o jogador deve jogar para regar a planta
    private float waterToCarrot = 10f; // quantidade de água necessária para a planta virar uma cenoura

    // Start is called before the first frame update
    void Start()
    {
        initialDigAmount = digAmount;    
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        playerItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerItems>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (watering)
            waterAmount += 10 * Time.deltaTime;

        if (watering && isHole && waterAmount >= waterToCarrot && !isCarrot)
        {
            audioSource.PlayOneShot(holeSFX);

            spriteRenderer.sprite = carrot;
            isCarrot = true;
        }

        if(isCarrot &&playerCanCollect)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                audioSource.PlayOneShot(carrotSFX);
                ResetToHole();
                playerItems.TotalCarrots++;
                isCarrot = false;
            }
        }
    }

    private void ResetToHole()
    {
        spriteRenderer.sprite = hole;
        isHole = true;
        watering = false;
        waterAmount = 0;
    }

    public void OnHit()
    {
        digAmount--;

        if (digAmount <= 0)
        {
            spriteRenderer.sprite = hole;
            isHole = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shovel"))
            OnHit();

        if (collision.CompareTag("Player"))
            playerCanCollect = true;

        if (collision.CompareTag("BucketWater") && isHole)
            watering = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BucketWater"))
            watering = false;

        if (collision.CompareTag("Player"))
            playerCanCollect = false;
    }
}
