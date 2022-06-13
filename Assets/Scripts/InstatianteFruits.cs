using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstatianteFruits : MonoBehaviour
{
    //gameobject of type fruits
    [SerializeField] private GameObject[] Array_Fruits;
    //private string[] FruitsNames() { return new  string[] { ("Apple", "Pineapple", "Cherries", "Melon", "Orange" }; }
    
    [SerializeField] private Vector3[] oneFruitPosition;
    //spawn fruits vector3 range
    private float[] spawnFruitPosition;
    [Header("Represent Size Array of the multipleFruits same element")]
    [SerializeField] private int[] NewFruitSpawn; 
    [Header("X represent First Fruit Pos , Y reprensent commun height, Z = 0")]
    [SerializeField] private Vector3[] multipleFruits;
    private int orangeCount = 0;

    void Start()
    {
        OrderFruitArrayPriority();
        //only one fruit
        if (oneFruitPosition.Length > 0)
        {
            foreach(var position in oneFruitPosition)
            {
                InstantiateFruits(position);
            }
        }
        //multiple fruits
        //this fonction wont work if one of the 3 array dont have the same lenght
        if (multipleFruits.Length > 0)
        {
            for (int i = 0; i < NewFruitSpawn.Length; i++)
            {
                StartPositionFruit(NewFruitSpawn[i], multipleFruits[i].x);
                FruitSpawn(NewFruitSpawn[i], multipleFruits[i].y);
            }
        }
    }


    private void OrderFruitArrayPriority()
    {
        string[] FruitsNamesOrder = new string[] { "Apple", "Pineapple", "Cherries", "Melon", "Orange" };

        int count = 0;
        int pos = 0;
        while (count< FruitsNamesOrder.Length-1)
        {
            var fruit = Array_Fruits[pos];
            if (fruit.name == FruitsNamesOrder[count])
            {
                var temp = Array_Fruits[count];
                Array_Fruits[count] = fruit;
                Array_Fruits[pos] = temp;

                pos = 0;
                count++;
                continue;
            }
            pos++;
        }
    }
    private void StartPositionFruit(int array_length, float firstValue)
    {
        spawnFruitPosition = new float[array_length];
        spawnFruitPosition[0] = firstValue;
        for (int i = 1; i < array_length; i++)
        {
            float rng_PosX = Random.Range(6, 10);
             spawnFruitPosition[i] = spawnFruitPosition[i-1] + rng_PosX;
        }
    }
    private void FruitSpawn(int array_length, float FuitPosY)
    {
        for (int i = 0; i < array_length; i++)
        {
            InstantiateFruits( i, FuitPosY);
        }
    }

    private int RandomFruit()
    {
            int fruitChance = Random.Range(0, 100);
            
            if(fruitChance >= 0 && fruitChance <= 30)
            {
                return 0;
            }
            else
            if (fruitChance > 30   && fruitChance <= 60 )
            {
                return 1;
            }
            else
            if (fruitChance > 60   && fruitChance <= 85 )
            {
                return 2;
            }
            else
            if (fruitChance > 85  && fruitChance < 98 )
            {
                return 3;
            }
            else 
            {
                if (orangeCount > 1) return 3; 

                orangeCount++;
                return 4;
            }
    }
   private void InstantiateFruits(int i,float firstYValue)
    {
        Instantiate(Array_Fruits[RandomFruit()], new Vector3(spawnFruitPosition[i], firstYValue, 0), Quaternion.identity);
    } 
    private void InstantiateFruits(Vector3 pos)
    {
        Instantiate(Array_Fruits[RandomFruit()], pos, Quaternion.identity);
    }
}
