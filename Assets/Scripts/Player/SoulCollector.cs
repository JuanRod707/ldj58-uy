using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Config;
using Assets.Scripts.Director;
using Assets.Scripts.Entities;
using Assets.Scripts.NPCs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    public class SoulCollector : MonoBehaviour
    {
        [SerializeField] float deliveryCooldown;
        [SerializeField] private DeathLaser deathLaser;
        [SerializeField] private float killCooldown;
        [SerializeField] Animator animator;

        PortalProvider portals;
        List<Soul> collectedSouls = new List<Soul>();
        SoulProvider soulProvider;
        EnemyInitializer enemyProvider;
        Battle battle;
        Transform lastInLine;
        PlayerInput input;
        float currentDamageCooldown;
        float portalProximity;
        private float collectDistance;
        Stats stats;


        public int SoulCount => collectedSouls.Count();

        public void Initialize(GameplayConfig config, Stats stats, SoulProvider soulProvider, PortalProvider portals,  Battle battle, EnemyInitializer enemyProvider, float maxDistance)
        {
            this.portals = portals;
            this.soulProvider = soulProvider;
            this.battle = battle;
            this.enemyProvider = enemyProvider;
            this.stats = stats;

            collectDistance = maxDistance;

            portalProximity = config.PortalPullDistance;
            lastInLine = transform;
        }

        void Update()
        {
            DetectPortal();

            if (currentDamageCooldown < killCooldown)
            {
                currentDamageCooldown += Time.deltaTime;
            }
        }

        void DetectPortal()
        {
            if (collectedSouls.Any() & portals.AnyInRange(transform.position, portalProximity))
            {
                var portal = portals.GetClosestTo(transform.position);
                var candidate = collectedSouls.Last();

                candidate.DeliverToPortal(portal);
                collectedSouls.Remove(candidate);
                lastInLine = collectedSouls.Any() ? collectedSouls.Last().transform : transform;

                enabled = false;
                Invoke("Continue", deliveryCooldown);
            }
        }

        public void HoldAttack()
        {
            if (AnySoulInRange && currentDamageCooldown >= killCooldown)
            {
                if (EnemyInRange) 
                    battle.StartCombat();

                Soul soul = soulProvider.GetClosestTo(transform.position);
                
                if (soul.CurrentHealth > 0)
                {
                    soul.Damage(stats.CaptureRate);
                    animator.SetBool("Attacking", true);
                    deathLaser.ThrowLaser(soul.transform.position, killCooldown);
                }
                else
                {
                    AddSoulToLine(soul);
                    animator.SetBool("Attacking", false);
                }

                currentDamageCooldown = 0;
            }
        }

        private bool EnemyInRange => enemyProvider.CheckIfAnyClose(transform.position, collectDistance);
        

        void Continue() => 
            enabled = true;

        bool AnySoulInRange => soulProvider.AnyInRange(transform.position, collectDistance);
        
        private void AddSoulToLine(Soul soul)
        {
            soulProvider.RemoveSoul(soul);
            collectedSouls.Add(soul);

            soul.SetFollowing(lastInLine);
            lastInLine = soul.transform;
        }

        private IEnumerator DamageSoul()
        {
            yield return new WaitForSeconds(currentDamageCooldown);
        }

        public Soul ClosestSoulTo(Vector3 where) => 
            collectedSouls.OrderBy(cs => Vector3.Distance(where, cs.transform.position)).First();

        public bool AnyCollectedSoulInRange(Vector3 where, float distance) => 
            collectedSouls.Any(cs => Vector3.Distance(where, cs.transform.position) < distance);

        public void RiftSoul(Rift rift, Soul riftedSoul)
        {
            var severedSouls =
                collectedSouls.TakeWhile(s => collectedSouls.IndexOf(s) > collectedSouls.IndexOf(riftedSoul));

            foreach (var ss in severedSouls)
            {
                collectedSouls.Remove(ss);
                ss.Detach();
            }

            riftedSoul.DeliverToRift(rift);
            collectedSouls.Remove(riftedSoul);
            lastInLine = collectedSouls.Any() ? collectedSouls.Last().transform : transform;
        }
    }
}
