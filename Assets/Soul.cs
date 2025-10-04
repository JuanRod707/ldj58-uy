using System;
using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    private Transform previousNode;
    private SoulState state;
    
    public Transform PreviousNode => previousNode; 
    public SoulState State => state;

    public void SetFollowing(Transform follow)
    {
        if (state != SoulState.Waiting) return;
        
        previousNode = follow;
        state = SoulState.Following;
        transform.position = previousNode.position + (transform.position - previousNode.position).normalized * minDistance;
    }
    
    public enum SoulState
    { 
        Waiting,
        Following
    }
    
    private void FixedUpdate()
    {
        if (previousNode is null) return;

        if (state == SoulState.Following)
        {
            if (Vector3.Distance(transform.position, previousNode.position) < maxDistance)
            {
                transform.position = previousNode.position + (transform.position - previousNode.position).normalized * minDistance;
            }
        } 
    }
}
