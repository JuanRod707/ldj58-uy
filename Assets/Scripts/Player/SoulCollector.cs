using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Director;
using Assets.Scripts.NPCs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    public class SoulCollector : MonoBehaviour
    {
        [SerializeField] float portalProximity;
        [SerializeField] float deliveryCooldown;
        [SerializeField] private DeathLaser deathLaser;
        [SerializeField] private float collectDistance;
        [SerializeField] private float killCooldown;
        [SerializeField] private int damagePerCooldown;

        PortalProvider portals;
        List<Soul> collectedSouls = new List<Soul>();
        SoulProvider soulProvider;
        Transform lastInLine;
        PlayerInput input;
        float currentDamageCooldown;

        public void Initialize(SoulProvider soulProvider, PortalProvider portals)
        {
            this.portals = portals;
            this.soulProvider = soulProvider;

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
                //hablar con el enemy provider para saber si ese alma tiene algun enemigo cerca, si es asi llamar a 
                //"ENTIDAD AUN DESCONOCIDA" para cambiar el input manejado

                Soul soul = soulProvider.GetClosestTo(transform.position);
                
                deathLaser.ThrowLaser(soul.transform.position, killCooldown);
                if (soul.CurrentHealth > 0)
                {
                    soul.Damage(damagePerCooldown);
                }
                else
                {
                    AddSoulToLine(soul);
                }

                currentDamageCooldown = 0;
            }
        }

        void Continue() => 
            enabled = true;
        
        private bool AnySoulInRange => soulProvider.AnyInRange(transform.position, collectDistance);

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
    }
}
