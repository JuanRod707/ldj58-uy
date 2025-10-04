using UnityEngine;

namespace Assets.Scripts.Common
{
    public class AutoFacing : MonoBehaviour
    {
        [SerializeField] Vector3 targetEuler;

        void Update() => transform.rotation = Quaternion.Euler(targetEuler);

    }
}
