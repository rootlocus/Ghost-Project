using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementV2 : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float distancePerGrid = 0.16f;
    [SerializeField] float nextDistanceCheck = 0.08f;
    [SerializeField] float circleSize = 0.04f;
    [SerializeField] Transform movePoint;
    [SerializeField] LayerMask blockingLayer;
    [SerializeField] bool isFreeze = false;
    [SerializeField] bool onGizmos = false;
    [SerializeField] Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        movePoint.parent = null; // moves the point outside
    }

    void FixedUpdate()
    {
        if (!isFreeze)
        {
            GridMovement();
        }
    }

    IEnumerator CoFreezePlayerTemporary(float wait)
    {
        isFreeze = true;

        yield return new WaitForSeconds(wait);

        isFreeze = false;
    }

    public bool CheckIfMoving()
    {
        if ( Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0 )
        {
            return true;
        } 
        return false;
    }

    public void FreezePlayerTemporary(float time)
    {
        StartCoroutine(CoFreezePlayerTemporary(time));
    }

    public void FreezePlayer()
    {
        isFreeze = true;
    }

    public void UnfreezePlayer()
    {
        isFreeze = false;
    }

    void GridMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) == 0) // if havent reach to point
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * nextDistanceCheck, 0f, 0f), circleSize, blockingLayer))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * distancePerGrid, 0f, 0f);
                }
                SetHorizontalAnimation();
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * nextDistanceCheck, 0f), circleSize, blockingLayer))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * distancePerGrid, 0f);
                }
                SetVerticalAnimation();
            }
        }

        SetIdleAnimation();
    }

    void SetIdleAnimation()
    {
        float hasInput = Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f ? 1 : 0;
        animator.SetFloat("Speed", hasInput);
    }

    void SetVerticalAnimation()
    {
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        bool isFacingUp = Input.GetAxisRaw("Vertical") == 1;
        animator.SetFloat("Direction", isFacingUp ? 0 : 0.3f);
    }

    void SetHorizontalAnimation()
    {
        animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("Vertical", 0);
        bool isFacingRight = Input.GetAxisRaw("Horizontal") == 1;
        animator.SetFloat("Direction", isFacingRight ? 0.5f : 0.8f);
    }

    void OnDrawGizmos()
    {
        if (onGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, circleSize);
        }
    }

}
