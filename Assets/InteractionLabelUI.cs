using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionLabelUI : MonoBehaviour
{
    [SerializeField] TMP_Text interactionLabel;
 
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Furniture" && !collision.gameObject.GetComponent<Furniture>().GetIsChecked())
        {
            interactionLabel.enabled = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Furniture" && !collision.gameObject.GetComponent<Furniture>().GetIsChecked())
        {
            interactionLabel.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Furniture")
        {
            interactionLabel.enabled = false;
        }
    }
}
