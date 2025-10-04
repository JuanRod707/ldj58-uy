using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem; 
using Assets.Scripts.Common;
using Assets.Scripts.Director;
using Assets.Scripts.NPCs;

namespace Assets.Scripts.Player
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Stats stats;
        [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] SoulCollector collector;
        
        public void Initialize(SoulProvider soulProvider, PortalProvider portals)
        {  
            collector.Initialize(soulProvider, portals, playerInput);
        }
        
        public void FixedUpdate() => 
            MovePlayer();


        private void MovePlayer()
        {
            if (playerInput.actions["Move"].IsPressed())
            {
                Vector3 inputVector = playerInput.actions["Move"].ReadValue<Vector2>() *
                                      (Time.fixedDeltaTime * (stats.GetTotalSpeed));
                agent.Move(new Vector3(inputVector.x, 0, inputVector.y));
            }
        }
    }
}