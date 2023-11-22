using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] private Image waterFilledUI;
    [SerializeField] private Image woodFilledUI;
    [SerializeField] private Image carrotFilledUI;
    [SerializeField] private Image fishFilledUI;

    [Header("Tools")]
    [SerializeField] private Image AxeImage;
    [SerializeField] private Image ShovelImage;
    [SerializeField] private Image BucketImage;
    [SerializeField] private Color colorTransparent;
    [SerializeField] private Color colorNormal;

    [Header("References")]
    [SerializeField] private PlayerItems playerItems;
    [SerializeField] private Player player;

    private void Awake()
    {
        if (playerItems == null)
            playerItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerItems>();
    }

    // Start is called before the first frame update
    void Start()
    {
        waterFilledUI.fillAmount = 0;
        woodFilledUI.fillAmount = 0;
        carrotFilledUI.fillAmount = 0;    
        fishFilledUI.fillAmount = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        waterFilledUI.fillAmount = playerItems.TotalWater / playerItems.WaterLimit;
        woodFilledUI.fillAmount = (float)playerItems.TotalWood / playerItems.WoodLimit;
        carrotFilledUI.fillAmount = (float)playerItems.TotalCarrots / playerItems.CarrotLimit;
        fishFilledUI.fillAmount = (float)playerItems.TotalFish / playerItems.FishLimit;

        if (player.weapon == Player.Weapon.Axe)
        {
            AxeImage.color = colorNormal;
            ShovelImage.color = colorTransparent;
            BucketImage.color = colorTransparent;
        } else if (player.weapon == Player.Weapon.Shovel)
        {
            AxeImage.color = colorTransparent;
            ShovelImage.color = colorNormal;
            BucketImage.color = colorTransparent;
        } else if (player.weapon == Player.Weapon.WaterBucket)
        {
            AxeImage.color = colorTransparent;
            ShovelImage.color = colorTransparent;
            BucketImage.color = colorNormal;
        }
    }
}
