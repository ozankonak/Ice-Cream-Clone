using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status { Current, Lose, Win}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Status gameStatus = Status.Current;

    public float MaxNumberOfSpawn { get; } = 120;
    public float AmountOfMilk { get; set; }
    public float AmountOfHoney { get; set; }
    public float AmountOfChocolate { get; set; }

    [SerializeField] private int desiredMilkAmount;
    [SerializeField] private int desiredHoneyAmount;
    [SerializeField] private int desiredChocolateAmount;

    public bool CanSpawn { get; set; } = true;

    private bool anyMilkInMatch => desiredMilkAmount > 0;
    private bool anyHoneyInMatch => desiredHoneyAmount > 0;
    private bool anyChocolateInMatch => desiredChocolateAmount > 0;

    private float average;

    private bool oneStar = false;
    private bool twoStar = false;
    private bool threeStar = false;

    private bool checkMatch = true;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        MatchSystem();
    }

    private void MatchSystem()
    {
        if (!CanSpawn && checkMatch)
        {
            StartCoroutine(StartMatchSystem());
            checkMatch = false;
        }

    }

    IEnumerator StartMatchSystem()
    {
        yield return new WaitForSecondsRealtime(3f);

        float averageOfMilk = CheckAverage(anyMilkInMatch, AmountOfMilk, desiredMilkAmount);
        float averageOfHoney = CheckAverage(anyHoneyInMatch, AmountOfHoney, desiredHoneyAmount);
        float averageOfChocolate = CheckAverage(anyChocolateInMatch, AmountOfChocolate, desiredChocolateAmount);


        average = ( averageOfMilk+ averageOfHoney + averageOfChocolate) * 100;
        Debug.Log(average.ToString());

        if (average >= 33.3f && average <= 50f)
            oneStar = true;
        else if (average > 50f  && average <= 66.6f)
            twoStar = true;
        else if (average > 66.6f && average <= 100.0f)
            threeStar = true;
        else
        {
            oneStar = false;
            twoStar = false;
            threeStar = false;
        }
        GiveStars();

        UIManager.instance.glassGameObject.SetActive(false);
        UIManager.instance.levelCompletedPanel.SetActive(true);
        UIManager.instance.matchRateText.text = "Match Rate : " + ((int)average).ToString();

    }


    private float CheckAverage(bool anyProductInGlass, float amountOfProduct, float desiredProduct)
    {
        if (anyProductInGlass && amountOfProduct != 0)
        {
            if (amountOfProduct > desiredProduct)
                amountOfProduct = desiredProduct;

            float average = (amountOfProduct / desiredProduct) * (desiredProduct / MaxNumberOfSpawn);

            return average;
        }
        else
            return 0;
    }


    private void GiveStars()
    {

        if (oneStar) 
        {
                UIManager.instance.stars[0].enabled = true;
                UIManager.instance.stars[1].enabled = false;
                UIManager.instance.stars[2].enabled = false;
        }
        else if (twoStar)
        {
                UIManager.instance.stars[0].enabled = true;
                UIManager.instance.stars[1].enabled = true;
                UIManager.instance.stars[2].enabled = false;
        }
        else if (threeStar) 
        {
                UIManager.instance.stars[0].enabled = true;
                UIManager.instance.stars[1].enabled = true;
                UIManager.instance.stars[2].enabled = true;
        }
        else
        {
                UIManager.instance.stars[0].enabled = false;
                UIManager.instance.stars[1].enabled = false;
                UIManager.instance.stars[2].enabled = false;
        }
    }

    //ilk resim 50 çukalata 70 bal
}
