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

        PortalProvider portals;
        List<Soul> collectedSouls = new List<Soul>();
        SoulProvider soulProvider;
        Transform lastInLine;
        PlayerInput input;
        float currentKillCooldown;

        public void Initialize(SoulProvider soulProvider, PortalProvider portals, PlayerInput input)
        {
            this.portals = portals;
            this.soulProvider = soulProvider;
            this.input = input;

            lastInLine = transform;
            input.actions["Attack"].performed += (InputAction.CallbackContext ctx) => AttemptAddSoul();
        }

        void Update()
        {
            HandleInput();
            DetectPortal();

            if (currentKillCooldown < killCooldown)
            {
                currentKillCooldown += Time.deltaTime;
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

        void HandleInput()
        {
            if (input.actions["Attack"].IsPressed() && currentKillCooldown >= killCooldown)
            {
                AttemptAddSoul();
                currentKillCooldown = 0f;
            }
        }

        void Continue() => 
            enabled = true;
        
        private void AttemptAddSoul()
        {
            if (soulProvider.AnyInRange(transform.position, collectDistance))
            {
                Soul soul = soulProvider.GetClosestTo(transform.position);
                soulProvider.RemoveSoul(soul);
                deathLaser.ThrowLaser(soul.transform.position, killCooldown);
                collectedSouls.Add(soul);

                soul.SetFollowing(lastInLine);
                lastInLine = soul.transform;
            }
        }
    }
}
