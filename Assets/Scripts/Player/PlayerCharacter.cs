using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Assets.Scripts.Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private PlayerStats stats;
        [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private LayerMask layerMask;

        private Soul currentSoulToAdd = null;
        public void Initialize()
        { 
            StartCoroutine(Detect());
            playerInput.actions["Attack"].performed += (InputAction.CallbackContext ctx) => AddSoul();
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
                                      (Time.fixedDeltaTime * (stats.GetTotalSpeed));
                agent.Move(new Vector3(inputVector.x, 0, inputVector.y));
            }
        }

        private IEnumerator Detect()
        {
            while (true)
            {
                if (Physics.SphereCast(transform.position, stats.DetectionRadius, transform.forward, out var hit, 
                        stats.DetectionRadius))
                { 
                    currentSoulToAdd = hit.collider.gameObject.GetComponent<Soul>();
                }
                yield return new WaitForSeconds(stats.DetectionSpeed);
            } 
        }

        private void AddSoul()
        {
            if(currentSoulToAdd is null) return;
            
            if (stats.Souls.Count == 0)
            { 
                currentSoulToAdd.SetFollowing(transform);
                stats.Souls.Add(currentSoulToAdd); 
            }
            else if(!stats.Souls.Contains(currentSoulToAdd))
            {
                currentSoulToAdd.SetFollowing(stats.Souls.Last().transform);
                stats.Souls.Add(currentSoulToAdd);
            }
        }
    }
}