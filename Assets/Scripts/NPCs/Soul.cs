using System;
using System.Collections;
using Assets.Scripts.Common;
using Assets.Scripts.Entities;
using UnityEditor.Experimental.GraphView;
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

        [SerializeField] AudioHelper takenSfx;

        float currentHealth;
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
        public void SetFollowing(Transform target)
        {
            takenSfx.PlayRandom();
            follow.StartFollowing(target);
        }

        public void DeliverToPortal(Portal portal)
        {
            portal.OnDeliver();
            Detach();
            StartCoroutine(DeliverSoulTo(portal.transform));
        }

        IEnumerator DeliverSoulTo(Transform where)
        {
            var startingDistance = Vector3.Distance(transform.position, where.position);

            while (Vector3.Distance(transform.position, where.position) > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, where.position, suctionFactor);
                transform.localScale = Vector3.one * Vector3.Distance(transform.position, where.position) / startingDistance;
                yield return null;
            }
            
            Destroy(gameObject);
        }

        public void  Detach() => 
            follow.enabled = false;

        public void DeliverToRift(Rift rift)
        {
            Detach();
            StartCoroutine(DeliverSoulTo(rift.transform));
        }
    }
}