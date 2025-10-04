using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.NPCs;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Director
{
    public class NPCInitializer : MonoBehaviour
    {
        [SerializeField] NavigationProvider navigation;
        [SerializeField] int npcAmount;
        [SerializeField] float mapDimension;
        [SerializeField] Transform npcContainer;

        [SerializeField] CivAI civPrefab;

        NavMeshData nm;
        NavMeshSurface nms;
        List<CivAI> civs = new List<CivAI>();

        public void Initialize()
        {
            navigation.Initialize();
            SpawnCivs();
        }

        void SpawnCivs()
        {
            foreach (var _ in Enumerable.Range(0, npcAmount))
            {
                var randomPoint = new Vector3(Random.Range(-mapDimension, mapDimension), 0,
                    Random.Range(-mapDimension, mapDimension));

                var civ = Instantiate(civPrefab, npcContainer);
                civ.transform.position = randomPoint;
                civ.Initialize(navigation);
                civs.Add(civ);
            }
        }

        public CivAI GetClosestTo(Vector3 where, float maxDistance) =>
            civs
                .Where(c => Vector3.Distance(c.transform.position, where) < maxDistance)
                .OrderBy(c => Vector3.Distance(c.transform.position, where))
                .First();
    }
}
