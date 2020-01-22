using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Milk")
        {
            other.tag = "MilkInGlass";
            other.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
            other.GetComponent<Rigidbody2D>().velocity = other.GetComponent<Rigidbody2D>().velocity / 4;
            GameManager.instance.AmountOfMilk++;
        }
        else if (other.transform.tag == "Honey")
        {
            other.tag = "HoneyInGlass";
            other.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
            other.GetComponent<Rigidbody2D>().velocity = other.GetComponent<Rigidbody2D>().velocity / 4;
            GameManager.instance.AmountOfHoney++;

        }
        else if (other.transform.tag == "Chocolate")
        {
            other.tag = "ChocolateInGlass";
            other.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
            other.GetComponent<Rigidbody2D>().velocity = other.GetComponent<Rigidbody2D>().velocity / 4;
            GameManager.instance.AmountOfChocolate++;
        }
    }

}
