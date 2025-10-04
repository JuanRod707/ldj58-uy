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
        [SerializeField] float suctionFactor;
        [SerializeField] private float maxHealth;
        [SerializeField] private SpriteRenderer healthSprite;

        private float currentHealth;
        public float CurrentHealth => currentHealth;
        
        public void Summon()
        {
            currentHealth = maxHealth;
            gameObject.SetActive(true);
            follow.Initialize(minDistance, maxDistance);
        }

        public void Damage(float amount)
        {
            currentHealth -= amount;
            healthSprite.size = Vector2.one * currentHealth / maxHealth;
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
                transform.position = Vector3.Lerp(transform.position, portal.position, suctionFactor);
                transform.localScale = Vector3.one * Vector3.Distance(transform.position, portal.position) / startingDistance;
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}