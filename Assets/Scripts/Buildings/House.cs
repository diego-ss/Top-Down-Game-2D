using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [Header("Amounts")]
    [SerializeField] private float timeAmount;
    [SerializeField] private int woodAmount;
    [SerializeField] private Color transparentColor;
    [SerializeField] private Color visibleColor;

    [Header("References")]
    [SerializeField] private GameObject playerPosition;
    [SerializeField] private GameObject collider;
    [SerializeField] private SpriteRenderer houseSprite;

    private bool playerNextToArea = false;
    private bool isBuilding;
    private float timeCounter = 0f;
    private GameObject player;
    private PlayerAnim playerAnim;
    private PlayerItems playerItems;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnim = player.GetComponent<PlayerAnim>();   
        playerItems = player.GetComponent<PlayerItems>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNextToArea && Input.GetKeyDown(KeyCode.E))
        {
            if (playerItems.UseWood(woodAmount))
            {
                isBuilding = true;
                player.transform.position = playerPosition.transform.position;
                player.transform.rotation = playerPosition.transform.rotation;
                playerAnim.OnHammeringStarted();
                houseSprite.color = transparentColor;
            }      
        }

        if (isBuilding)
        {
            timeCounter += Time.deltaTime;

            // casa construida
            if(timeCounter >= timeAmount)
            {
                houseSprite.color = visibleColor;
                collider.SetActive(true);
                playerAnim.OnHammeringEnded();
                isBuilding = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNextToArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNextToArea = false;
        }
    }
}
