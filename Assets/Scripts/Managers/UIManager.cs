using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables

    public static UIManager instance;

    public Slider ProgressionSlider;
    public Image[] stars;

    public GameObject glassGameObject;
    public GameObject levelCompletedPanel;

    public Text coinText;
    public Text matchRateText;
    public bool MilkButtonPressed { get; private set; }
    public bool HoneyButtonPressed { get; private set; }
    public bool ChocolateButtonPressed { get; private set; }

    #endregion

    #region Unity Functions
    private void Awake()
    {
        instance = this;
        levelCompletedPanel.SetActive(false);
    }

    private void Start()
    {
        ProgressionSlider.value = 0;

        foreach (var image in stars)
        {
            image.enabled = false;
        }
    }

    #endregion

    #region Button Configuration

    public void OnPointingDownMilkButton()
    {
        MilkButtonPressed = true;
    }

    public void OnPointingUpMilkButton()
    {
        MilkButtonPressed = false;
    }

    public void OnPointingDownHoneyButton()
    {
        HoneyButtonPressed = true;
    }

    public void OnPointingUpHoneyButton()
    {
        HoneyButtonPressed = false;
    }

    public void OnPointingDownChocolateButton()
    {
        ChocolateButtonPressed = true;
    }

    public void OnPointingUpChocolateButton()
    {
        ChocolateButtonPressed = false;
    }

    #endregion

} 
