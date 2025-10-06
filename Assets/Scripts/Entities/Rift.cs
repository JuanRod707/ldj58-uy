using Assets.Scripts.Common;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Rift : MonoBehaviour
    {
        [SerializeField] float lifeTime;
        [SerializeField] AudioHelper riftedSfx;

        float pullDistance;
        SoulCollector soulCollector;

        public void Initialize(float pullDistance)
        {
            this.soulCollector = FindFirstObjectByType<SoulCollector>();
            this.pullDistance = pullDistance;
            Invoke("Close", lifeTime);
        }

        void Update()
        {
            if (soulCollector.AnyCollectedSoulInRange(transform.position, pullDistance))
            {
                var candidate = soulCollector.ClosestSoulTo(transform.position);
                soulCollector.RiftSoul(this, candidate);
                riftedSfx.PlayRandomOneShot();
            }
        }

        void Close() => 
            Destroy(gameObject);
    }
}
