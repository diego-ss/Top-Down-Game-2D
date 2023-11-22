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
    
    public float WaterLimit = 50;
    public int WoodLimit = 10;
    public int CarrotLimit = 5;

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
}
