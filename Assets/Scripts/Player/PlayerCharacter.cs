using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private Stats playerStats;
        [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
        [SerializeField] private PlayerInput playerInput;

        private void Start()
        {
            //TODO move to initializer
            StartCoroutine(Detect());
        }

        public void FixedUpdate()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            if (playerInput.actions["Move"].IsPressed())
            {
                Vector3 inputVector = playerInput.actions["Move"].ReadValue<Vector2>() *
                                      (Time.fixedDeltaTime * (playerStats.Speed));
                agent.Move(new Vector3(inputVector.x, 0, inputVector.y));
            }
        }

        private IEnumerator Detect()
        {
            if (Physics.SphereCast(transform.position, playerStats.DetectionRadius, transform.forward, out var hit))
            {
                if (hit.collider.CompareTag("Soul"))
                {
                    
                }
            }

            yield return new WaitForSeconds(playerStats.DetectionSpeed);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, playerStats.DetectionRadius);
            
        }
    }
}

[System.Serializable]
public struct Stats
{
    public int CurrentSoulsAmount;
    public float Speed;
    public float DetectionRadius;
    public float DetectionSpeed;
}