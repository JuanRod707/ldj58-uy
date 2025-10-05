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
        [SerializeField] SoulCollector collector;
        
        public void Initialize(SoulProvider soulProvider, PortalProvider portals)
        {  
            collector.Initialize(soulProvider, portals);
        }
        
        public void MovePlayer(Vector3 inputVector)
        {

                agent.Move(new Vector3(inputVector.x, 0, inputVector.y) * stats.GetTotalSpeed);
        }
    }
}