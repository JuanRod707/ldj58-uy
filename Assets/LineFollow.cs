using UnityEngine;

namespace Assets
{
    public class LineFollow : MonoBehaviour
    {
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
            enabled = true;
        }
    }
}