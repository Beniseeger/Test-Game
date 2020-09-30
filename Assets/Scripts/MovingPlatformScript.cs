using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    private Vector3 posA;

    private Vector3 posB;

    private Vector3 nextPosition;

    [SerializeField]
    private float speed;
    
    [SerializeField]
    private Transform childTransform;

    [SerializeField]
    private Transform transformB;

    private void Start()
    {
        posA = childTransform.localPosition;
        posB = transformB.localPosition;
        nextPosition = posB;
    }

    private void FixedUpdate()
    {
        Move();   
    }

    private void Move()
    {

        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPosition, speed * Time.deltaTime);

        if (Vector3.Distance(childTransform.localPosition, nextPosition) <= 0.1)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        nextPosition = nextPosition != posA ? posA : posB;
    }

}
