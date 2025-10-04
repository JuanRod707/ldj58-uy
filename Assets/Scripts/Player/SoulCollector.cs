using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Director;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class SoulCollector : MonoBehaviour
    {
        [SerializeField] float portalProximity;
        [SerializeField] float deliveryCooldown;

        PortalProvider portals;
        List<Soul> collectedSouls;

        public void Initialize(PortalProvider portals)
        {
            this.portals = portals;
        }

        void Update()
        {
            if (portals.AnyInRange(transform.position, portalProximity))
            {
                var portal = portals.GetClosestTo(transform.position);
                collectedSouls.Last().DeliverToPortal(portal);
                
                enabled = false;
                Invoke("Continue", deliveryCooldown);
            }
        }

        void RestAndContinue() => 
            enabled = true;

        //[SerializeField] PlayerInput playerInput;
        //SoulProvider soulProvider;

        //Transform lastInLine;
        //PlayerStats stats;

        //public void Initialize(SoulProvider provider, PlayerStats stats)
        //{
        //    this.stats = stats;
        //    this.soulProvider = provider;
            
        //    lastInLine = transform;
        //    playerInput.actions["Attack"].performed += (InputAction.CallbackContext ctx) => AttemptAddSoul();
        //}
        
        //private void AttemptAddSoul()
        //{
        //    if (soulProvider.AnyInRange(transform.position, stats.DetectionRadius))
        //    {
        //        Soul soul = soulProvider.GetClosestTo(transform.position);
        //        soul.SetFollowing(lastInLine);
        //        lastInLine = soul.transform;
        //    }
        //}
    }
}
