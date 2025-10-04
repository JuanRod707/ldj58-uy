using System.Collections.Generic;
using Assets.Scripts.NPCs;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Director
{
    public class NPCInitializer : MonoBehaviour
    {
        [SerializeField] NavigationProvider navigation;
        NavMeshData nm;
        NavMeshSurface nms;
        IEnumerable<CivAI> civs;

        public void Initialize()
        {
            navigation.Initialize();
            //SpawnCivs();

            civs = FindObjectsByType<CivAI>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

            foreach(var c in civs) 
                c.Initialize(navigation);
        }

        void SpawnCivs()
        {
            var rpV2 = Random.insideUnitCircle;
            var randomPoint = new Vector3(rpV2.x, 0, rpV2.y);
        }
    }
}
