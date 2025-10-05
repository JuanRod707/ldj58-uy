using UnityEngine;

namespace Assets.Scripts.NPCs
{
    public class LineFollow : MonoBehaviour
    {
        [SerializeField] SpriteRenderer sprite;

        private Transform target;
        double maxDistance;
        float minDistance;

        private void FixedUpdate()
        {
            if (target is null) return;

            if (Vector3.Distance(transform.position, target.position) < maxDistance)
            {
                transform.position = target.position +
                                     (transform.position - target.position).normalized * minDistance;
                sprite.flipX = (target.position - transform.position).x < 0;
            }

        }

        public void Initialize(float minDistance, float maxDistance)
        {
            this.minDistance = minDistance;
            this.maxDistance = maxDistance;
            enabled = false;
        }

        public void StartFollowing(Transform target)
        {
            this.target = target;
            transform.position = target.position;
            enabled = true;
        }
    }
}