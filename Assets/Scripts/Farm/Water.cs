using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private bool playerNextToWater = false;
    [SerializeField] private int waterMinAmount = 6;
    [SerializeField] private int waterMaxAmount = 12;
    [SerializeField] private int waterAmount;

    private PlayerItems playerItems;

    // Start is called before the first frame update
    void Start()
    {
        playerItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerItems>();
        waterAmount = Random.Range(waterMinAmount, waterMaxAmount);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerNextToWater && Input.GetKeyDown(KeyCode.E))
        {
            playerItems.AddWater(waterAmount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerNextToWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNextToWater = false;
        }
    }
}
