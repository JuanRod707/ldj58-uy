using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.NPCs
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] NavMeshAgent agent;
        [SerializeField] SpriteRenderer sprite;

        public void GoTo(Vector3 location)
        {
            agent.SetDestination(location);
            agent.isStopped = false;
        }

        void Update()
        {
            sprite.flipX = (agent.destination - transform.position).x > 0;
        }

        public void Stop()
        {
            agent.SetDestination(transform.position);
            agent.isStopped = true;
            agent.enabled = false;
        }
    }
}
