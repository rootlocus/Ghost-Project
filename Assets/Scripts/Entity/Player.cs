using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] bool isPlayerFreeze = false;
    [SerializeField] public int stress;

    BoxCollider2D boxCollider;
    Vector3 moveDelta;
    RaycastHit2D hit;
    float interactRadius = 0.1f;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        //TODO: maybe highlight object when nearby ?
        if (Input.GetKeyUp(KeyCode.Space)) // cant retrigger if player is busy
        {
            Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, interactRadius, LayerMask.GetMask("Interactables"));
            foreach (Collider2D collider2D in collider2DArray) {
                Interactable interactable = collider2D.GetComponent<Interactable>();

                if (interactable != null)
                {
                    interactable.Interact();
                }
                // Debug.Log("PLAYER COLLIDED WITH " + collider2D.gameObject.name);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isPlayerFreeze)
        {
            PlayerMovementFourDirection();
        }
        // PlayerMovementSmooth();
    }

    private void PlayerMovementFourDirection()
    {
        KeyCode key = KeyCode.None;
        if      (Input.GetKey (KeyCode.W))  key = KeyCode.W;
        else if (Input.GetKey (KeyCode.A))  key = KeyCode.A;
        else if (Input.GetKey (KeyCode.S))  key = KeyCode.S;
        else if (Input.GetKey (KeyCode.D))  key = KeyCode.D;

        if (Input.GetKeyUp (key))  key = KeyCode.None;

        Vector2 moveDelta = new Vector2(0,0); // (0, 0) by default
        switch (key) {
            case KeyCode.W:  moveDelta.y =  1;  break;
            case KeyCode.A:  moveDelta.x = -1;  break;
            case KeyCode.S:  moveDelta.y = -1;  break;
            case KeyCode.D:  moveDelta.x =  1;  break;
        }
        // here you use direction variable to set animator params etc.
        if (moveDelta.x > 0) {
            transform.localScale = Vector3.one;
        } else if (moveDelta.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }

    private void PlayerMovementSmooth()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector3(x,y,0);

        if (moveDelta.x > 0) {
            transform.localScale = Vector3.one;
        } else if(moveDelta.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }

    public void ToggleFreeze()
    {
        isPlayerFreeze = !isPlayerFreeze;
    }

    public void GainStress(int stressGain)
    {
        stress += stressGain;
    }

    public void LoseStress(int stressLost)
    {
        stress -= stressLost;
        if(stress < 0) {
            stress = 0;
        }
    }

    public void Death()
    {
        this.gameObject.transform.Rotate(new Vector3(0f, 0f, 90f));
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<Player>().enabled = false;
    }

}
