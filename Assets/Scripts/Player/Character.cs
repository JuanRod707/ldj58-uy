using UnityEngine;
using Assets.Scripts.Director;

namespace Assets.Scripts.Player
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Stats stats;
        [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
        [SerializeField] SoulCollector collector;
        [SerializeField] Animator animator;
        [SerializeField] SpriteRenderer sprite;
        
        public void Initialize(SoulProvider soulProvider, PortalProvider portals) => 
            collector.Initialize(soulProvider, portals);

        public void MovePlayer(Vector3 inputVector)
        {
            agent.Move(new Vector3(inputVector.x, 0, inputVector.y) * stats.GetTotalSpeed);
            animator.SetBool("Walking", true);
            
            if (inputVector.magnitude != 0)
            {
                sprite.flipX = inputVector.x > 0;
            }
        }

        public void Stop() => 
            animator.SetBool("Walking", false);
    }
}