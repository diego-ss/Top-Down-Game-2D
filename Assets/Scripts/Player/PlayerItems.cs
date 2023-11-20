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

    private int WaterLimit = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWater(int amount)
    {
        if(totalWater < WaterLimit)
        {
            totalWater = Mathf.Clamp(totalWater += amount, 0, WaterLimit);
            Debug.Log("Água adicionada.");
        } else
        {
            Debug.Log("Limite de água atingido!");
        }
    }
}
