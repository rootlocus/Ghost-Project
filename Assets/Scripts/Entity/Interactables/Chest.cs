using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int moneyAmount = 5;

    protected override void OnCollect()
    {
        // base.OnCollect();

        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            //TODO send show dialog event with data
            GameManager.instance.ShowText("+" + moneyAmount + " dollars", 25 , Color.yellow, transform.position, Vector3.up * 50, 1.5f); 
            Debug.Log("You gained " + moneyAmount + " dollars !");
        }
    }
}
