using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [SerializeField] private int totalWood;
    public int TotalWood { get => totalWood; set => totalWood = value; }

    [SerializeField] private float totalWater;
    public float TotalWater { get => totalWater; set => totalWater = value; }

    [SerializeField] private int totalCarrots;
    public int TotalCarrots { get => totalCarrots; set => totalCarrots = value; }

    [SerializeField] private int totalFish;
    public int TotalFish { get => totalFish; set => totalFish = value; }
    
    public float WaterLimit;
    public int WoodLimit;
    public int CarrotLimit;
    public int FishLimit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWater(float amount)
    {
        if(totalWater < WaterLimit)
            totalWater = Mathf.Clamp(totalWater += amount, 0, WaterLimit);
    }

    public void AddFish(int amount)
    {
        if(totalFish < FishLimit)
            totalFish = Mathf.Clamp(totalFish += amount, 0, totalFish);
    }
}
