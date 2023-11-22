using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingArea : MonoBehaviour
{
    [SerializeField] private bool playerNextToArea = false;
    [SerializeField] private int fishAmount;
    [SerializeField, Range(0,100)] private int chanceToGetFish;
    [SerializeField] private GameObject fishPrefab;

    [SerializeField] private int fishMinAmount;
    [SerializeField] private int fishMaxAmount;
    private PlayerItems playerItems;
    private PlayerAnim playerAnim;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerItems = player.GetComponent<PlayerItems>();
        playerAnim = player.GetComponent<PlayerAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNextToArea && Input.GetKeyDown(KeyCode.E))
            playerAnim.OnCastingStarted();
    }

    public void OnCasting()
    {
        int randomValue = Random.Range(1, 100);

        if (randomValue <= chanceToGetFish)
        {
            fishAmount = Random.Range(fishMinAmount, fishMaxAmount);
            
            for (int i = 0; i < fishAmount; i++)
                Instantiate(fishPrefab, player.transform.position + new Vector3(Random.Range(-2f, -1f),Random.Range(0f, 1f),0f), Quaternion.identity) ;
        } 
        else
        {
            Debug.Log("You didn't catch anything!");
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
