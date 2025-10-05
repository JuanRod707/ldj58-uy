using UnityEngine;
using Assets.Scripts.Director;
using Assets.Scripts.Config;

namespace Assets.Scripts.Player
{
    public class Character : MonoBehaviour
    {
        [SerializeField] UnityEngine.AI.NavMeshAgent agent;
        [SerializeField] SoulCollector collector;
        [SerializeField] Animator animator;
        [SerializeField] SpriteRenderer sprite;

        float baseSpeed;
        float speedCutPerSoul;

        public float CurrentSpeed => baseSpeed - (baseSpeed * speedCutPerSoul * collector.SoulCount);

        public void Initialize(GameplayConfig config, SoulProvider soulProvider, PortalProvider portals)
        {
            baseSpeed = config.GrimmySpeed;
            speedCutPerSoul = config.GrimmySpeedCutPerSoul;
            collector.Initialize(config, soulProvider, portals);
        }

        public void MovePlayer(Vector3 inputVector)
        {
            agent.Move(new Vector3(inputVector.x, 0, inputVector.y) * CurrentSpeed);
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