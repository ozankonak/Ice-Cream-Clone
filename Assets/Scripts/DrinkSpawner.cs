using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrinkSpawner : MonoBehaviour
{
    public GameObject milk, honey, chocolate;

    private float timeToSpawn = 0.2f;
    private float spawnTime;

    private int spawnedMilk;
    private int spawnedHoney;
    private int spawnedChocolate;

    private GameObject spawn = null;


    private void Update()
    {
        CheckForButtons();
        CheckForCanSpawn();
    }

    private void CheckForButtons()
    {
        if (spawnTime + timeToSpawn < Time.time && GameManager.instance.CanSpawn)
        {
            if (UIManager.instance.MilkButtonPressed)
            {
                spawn = Instantiate(this.milk, transform.position, Quaternion.identity);
                spawn.transform.SetParent(UIManager.instance.glassGameObject.transform);
                spawnedMilk++;
                spawnTime = Time.time;
            }
            else if (UIManager.instance.HoneyButtonPressed)
            {
                spawn = Instantiate(this.honey, transform.position, Quaternion.identity);
                spawn.transform.SetParent(UIManager.instance.glassGameObject.transform);
                spawnedHoney++;
                spawnTime = Time.time;
            }
            else if (UIManager.instance.ChocolateButtonPressed)
            {
                spawn = Instantiate(this.chocolate, transform.position, Quaternion.identity);
                spawn.transform.SetParent(UIManager.instance.glassGameObject.transform);
                spawnedChocolate++;
                spawnTime = Time.time;
            }
        }
    }

    private void CheckForCanSpawn()
    {
        UIManager.instance.ProgressionSlider.value = (spawnedMilk + spawnedHoney + spawnedChocolate) / GameManager.instance.MaxNumberOfSpawn;

        if (spawnedMilk + spawnedHoney + spawnedChocolate >= GameManager.instance.MaxNumberOfSpawn && GameManager.instance.CanSpawn)
        {
            Debug.Log("Spawned Milk: " + spawnedMilk + " Spawned Honey " + spawnedHoney + " Spawned Chocolate " + spawnedChocolate);
            GameManager.instance.CanSpawn = false;
        }
    }
}
