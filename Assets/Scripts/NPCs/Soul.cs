using System.Collections;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.NPCs
{
    public class Soul : MonoBehaviour
    {
        [SerializeField] private float minDistance;
        [SerializeField] float maxDistance;
        [SerializeField] LineFollow follow;

        public void Summon()
        {
            gameObject.SetActive(true);
            follow.Initialize(minDistance, maxDistance);
            
        }

        public void SetFollowing(Transform target) => 
            follow.StartFollowing(target);
        
        public void DeliverToPortal(Portal portal)
        {
            follow.enabled = false;
            StartCoroutine(DeliverSoulTo(portal.transform));
        }

        IEnumerator DeliverSoulTo(Transform portal)
        {
            var startingDistance = Vector3.Distance(transform.position, portal.position);

            while (Vector3.Distance(transform.position, portal.position) > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, portal.position, 0.2f);
                transform.localScale = Vector3.one * Vector3.Distance(transform.position, portal.position) / startingDistance;
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}