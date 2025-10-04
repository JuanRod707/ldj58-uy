using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.NPCs
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] NavMeshAgent agent;

        public void GoTo(Vector3 location)
        {
            agent.SetDestination(location);
            agent.isStopped = false;
        }
    }
}
