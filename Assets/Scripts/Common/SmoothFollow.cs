using UnityEngine;

namespace Assets.Scripts.Common
{
    public class SmoothFollow : MonoBehaviour
    {
        [SerializeField] Transform anchor;
        [SerializeField] float elasticity;

        void Update() => 
            transform.position = Vector3.Lerp(transform.position, anchor.position, elasticity);
    }
}
