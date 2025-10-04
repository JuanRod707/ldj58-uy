using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float speed = 1f;

    public void FixedUpdate()
    {
        if (playerInput.actions["Move"].IsPressed())
        {
            Vector3 inputVector = playerInput.actions["Move"].ReadValue<Vector2>() * (Time.fixedDeltaTime * speed);
            agent.Move(new Vector3(inputVector.x,0,inputVector.y));
        } 
        
    }

}