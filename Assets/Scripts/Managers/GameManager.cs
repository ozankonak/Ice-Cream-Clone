using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region Variables

    public float MaxNumberOfSpawn { get; } = 120;
    public float AmountOfMilk { get; set; }
    public float AmountOfHoney { get; set; }
    public float AmountOfChocolate { get; set; }

    [Header("Amount Part")]
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

    [Header("Coin Part")]
    //For Coins
    [SerializeField]
    private int amountOfCoinInThisLevel;
    private int currentCoin;
    private int totalCoin;

    #endregion

    #region Unity Functions

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("coin") != 0)
        {
            totalCoin = PlayerPrefs.GetInt("coin");
            UIManager.instance.coinText.text = PlayerPrefs.GetInt("coin").ToString();
        }
        else
            totalCoin = 0;

    }

    private void Update()
    {
        MatchSystem();
    }

    #endregion

    #region Private Functions

    
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
        
        //Add Coin 
        currentCoin = (int)(amountOfCoinInThisLevel * average /100);
        totalCoin += currentCoin;
        PlayerPrefs.SetInt("coin",totalCoin);
        UIManager.instance.coinText.text = PlayerPrefs.GetInt("coin").ToString();

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

    #endregion

    #region Public Functions

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion



}
