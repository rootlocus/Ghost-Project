using UnityEngine;

public class EMF : Item
{
    [SerializeField]
    private bool isActivated = false;

    public override void utilise()
    {
        isActivated = !isActivated;
        // TODO add sound when enemy near emf radius
        Debug.Log("EMF");
    }
}
