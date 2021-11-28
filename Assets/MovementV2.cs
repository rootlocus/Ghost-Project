using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
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

    void Start()
    {
        movePoint.parent = null; // moves the point outside
    }

    void Update()
    {
        if (!isFreeze)
        {
            GridMovement();
        }
    }

    private void GridMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * nextDistanceCheck, 0f, 0f), circleSize, blockingLayer))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * distancePerGrid, 0f, 0f);
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * nextDistanceCheck, 0f), circleSize, blockingLayer))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * distancePerGrid, 0f);
                }
            }
        }
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
