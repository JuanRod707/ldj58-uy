using System.Linq;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Director
{
    public class PortalProvider : MonoBehaviour
    {
        [SerializeField] Portal[] portals;
        [SerializeField] Progress progress;

        public void Initialize()
        {
            foreach (var p in portals) 
                p.Initialize(progress);
        }

        public bool AnyInRange(Vector3 where, float distance) =>
            portals.Any(p => Vector3.Distance(p.transform.position, where) <= distance);

        public Portal GetClosestTo(Vector3 where) =>
            portals.OrderBy(p => Vector3.Distance(p.transform.position, where)).First();
    }
}
