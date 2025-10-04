using UnityEngine;

namespace Assets
{
    public class Soul : MonoBehaviour
    {
        [SerializeField] private float minDistance;
        [SerializeField] float maxDistance;
        [SerializeField] LineFollow follow;

        void Start() =>
            follow.Initialize(minDistance, maxDistance);

        public void SetFollowing(Transform target) => 
            follow.StartFollowing(target);
    }
}