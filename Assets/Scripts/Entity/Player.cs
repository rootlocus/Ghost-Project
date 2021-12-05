using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] StressUI stressUi;
    [SerializeField] public int stress;
    [SerializeField] GameEvent OnLevelLose;
    float interactRadius = 0.1f;

    void Awake()
    {
        stressUi = GetComponentInChildren<StressUI>();    
    }

    void Update()
    {
        InteractInteractables();
    }

    private void InteractInteractables()
    {
        //TODO: maybe highlight object when nearby ?
        if (Input.GetKeyUp(KeyCode.Space)) // cant retrigger if player is busy
        {
            Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, interactRadius, LayerMask.GetMask("Interactables"));
            foreach (Collider2D collider2D in collider2DArray)
            {
                Interactable interactable = collider2D.GetComponent<Interactable>();

                if (interactable != null)
                {
                    interactable.Interact();
                }
                // Debug.Log("PLAYER COLLIDED WITH " + collider2D.gameObject.name);
            }
        }
    }

    public int GetStress()
    {
        return stress;
    }

    public void TakeDamage()
    {
        stress = --stress;
        stressUi.UpdateStressUi();
    }

    public void Death()
    {
        this.gameObject.transform.Rotate(new Vector3(0f, 0f, 90f));
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<Player>().enabled = false;
        OnLevelLose?.Raise();
    }

}
