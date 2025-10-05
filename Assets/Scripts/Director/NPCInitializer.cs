using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Config;
using Assets.Scripts.NPCs;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Director
{
    public class NPCInitializer : MonoBehaviour
    {
        [SerializeField] NavigationProvider navigation;
        [SerializeField] Transform npcContainer;

        [SerializeField] CivAI civPrefab;

        NavMeshData nm;
        NavMeshSurface nms;
        List<CivAI> civs = new List<CivAI>();
        float mapDimension;

        public void Initialize(GameplayConfig config)
        {
            mapDimension = config.MapSize;

            navigation.Initialize();
            SpawnCivs(config.NPCCount);
        }

        void SpawnCivs(int npcAmount)
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

        public CivAI GetRandomAlive() => civs.Where(c => c.Alive).PickOne();
    }
}
