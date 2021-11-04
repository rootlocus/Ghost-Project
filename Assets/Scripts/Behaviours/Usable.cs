using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable : MonoBehaviour
{
    public Sprite itemSprite;
    public bool isConsumable = true;
    private int quantity = 1;

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (quantity == 0) {
            Destroy(this.gameObject);
        }
    }
    
    protected virtual void onUse()
    {
        execute();

        if (isConsumable) 
        {
            quantity--;
        }
    }

    protected virtual void execute()
    {
        Debug.Log("Im used");
    }

    protected virtual void onDrop()
    {
        quantity--;
        Debug.Log("Dropped");
    }

}
