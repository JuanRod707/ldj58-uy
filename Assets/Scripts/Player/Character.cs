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
        [SerializeField] private float maxKillDistance;
        [SerializeField] private float killCooldown;
        [SerializeField] private DeathLaser deathLaser;
        
        private SoulProvider soulProvider;
        private float currentKillCooldown;
        
        public void Initialize(SoulProvider soulProvider)
        { 
            currentKillCooldown = 0f;
            this.soulProvider = soulProvider;
        }
        
        public void FixedUpdate()
        {
            MovePlayer();
        }

        private void Update()
        {
            if (playerInput.actions["Attack"].IsPressed() && currentKillCooldown >= killCooldown)
            {  
                if (!GetSoul(out Soul soulToAdd)) return;
                
                AddSoul(soulToAdd);
                currentKillCooldown = 0f;
            }

            if (currentKillCooldown < killCooldown)
            {
                currentKillCooldown += Time.deltaTime;
            }
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
        
        private void AddSoul(Soul soulToAdd)
        { 
            deathLaser.ThrowLaser(soulToAdd.transform.position, killCooldown);
            
            if (stats.Souls.Count == 0)
            { 
                soulToAdd.SetFollowing(transform);
                stats.Souls.Add(soulToAdd); 
            }
            else if(!stats.Souls.Contains(soulToAdd))
            {
                soulToAdd.SetFollowing(stats.Souls.Last().transform);
                stats.Souls.Add(soulToAdd);
            }
            
            soulProvider.RemoveSoul(soulToAdd);
        }

        private bool GetSoul(out Soul soulToAdd)
        {
            soulToAdd = soulProvider.GetClosestTo(transform.position, maxKillDistance);

            return soulToAdd != null;
        } 
    }
}