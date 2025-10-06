using System.Collections;
using Assets.Scripts.Common;
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
        [SerializeField] int combatDamage;
        [SerializeField] private Battle battle;
        [SerializeField] private float maxDistance;
        [SerializeField] AudioSource stunnedSfx;

        float baseSpeed;
        float speedCutPerSoul;
        int health;
        float currentStunTimeLeft;
        InputDirector inputDirector;
        
        
        public float CurrentSpeed => baseSpeed - (baseSpeed * speedCutPerSoul * collector.SoulCount); 
        public int CombatDamage => combatDamage;
        public Battle Battle => battle;
        public float CollectDistance => maxDistance;

        public void Initialize(GameplayConfig config, SoulProvider soulProvider, PortalProvider portals, InputDirector inputDirector, EnemyInitializer enemyProvider)
        {
            this.inputDirector = inputDirector;
            baseSpeed = config.GrimmySpeed;
            speedCutPerSoul = config.GrimmySpeedCutPerSoul;
            battle.Initialize(this, inputDirector,enemyProvider);
            collector.Initialize(config, soulProvider, portals, battle, enemyProvider, maxDistance);
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

        public void Stun(float time)
        {
            StartCoroutine(StunnedCoroutine(time));
            stunnedSfx.Play();
        }

        public void Stop() => 
            animator.SetBool("Walking", false);

        private IEnumerator StunnedCoroutine(float time)
        {
            animator.SetBool("Stun", true);
            yield return new WaitForSeconds(time);
            animator.SetBool("Stun", false);
            
            inputDirector.EnableRoam(true);
        }

        public void Attacking(bool attack)
        {
            animator.SetBool("Attacking", attack);
        }
    }
}