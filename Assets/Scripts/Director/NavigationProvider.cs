using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.NPCs;
using UnityEngine;

namespace Assets.Scripts.Director
{
    public class NavigationProvider : MonoBehaviour
    {
        IEnumerable<Transform> navPoints;

        public void Initialize() => 
            navPoints = GetComponentsInChildren<NavPoint>().Select(np => np.transform);

        public Transform GetRandomPoint() => navPoints.PickOne();
    }
}
