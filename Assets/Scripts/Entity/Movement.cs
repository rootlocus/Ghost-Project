using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Vector3 moveDelta;
    RaycastHit2D hit;

    [SerializeField] bool isEntityFreeze = false;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if (!isEntityFreeze)
        {
            MovementFourDirection();
        }
    }

    void MovementFourDirection()
    {
        KeyCode key = KeyCode.None;
        if (Input.GetKey(KeyCode.W)) key = KeyCode.W;
        else if (Input.GetKey(KeyCode.A)) key = KeyCode.A;
        else if (Input.GetKey(KeyCode.S)) key = KeyCode.S;
        else if (Input.GetKey(KeyCode.D)) key = KeyCode.D;

        if (Input.GetKeyUp(key)) key = KeyCode.None;

        Vector2 moveDelta = new Vector2(0, 0); // (0, 0) by default
        switch (key)
        {
            case KeyCode.W: moveDelta.y = 1; break;
            case KeyCode.A: moveDelta.x = -1; break;
            case KeyCode.S: moveDelta.y = -1; break;
            case KeyCode.D: moveDelta.x = 1; break;
        }
        // here you use direction variable to set animator params etc.
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
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

    //private void PlayerMovementSmooth()
    //{
    //    float x = Input.GetAxisRaw("Horizontal");
    //    float y = Input.GetAxisRaw("Vertical");

    //    moveDelta = new Vector3(x,y,0);

    //    if (moveDelta.x > 0) {
    //        transform.localScale = Vector3.one;
    //    } else if(moveDelta.x < 0) {
    //        transform.localScale = new Vector3(-1, 1, 1);
    //    }

    //    hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

    //    if (hit.collider == null)
    //    {
    //        transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
    //    }
    //    hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
    //    if (hit.collider == null)
    //    {
    //        transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
    //    }
    //}

    public void ToggleFreeze()
    {
        isEntityFreeze = !isEntityFreeze;
    }
}
