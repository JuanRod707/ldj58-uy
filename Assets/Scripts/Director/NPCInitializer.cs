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

        [SerializeField] CivAI[] civPrefabs;

        NavMeshData nm;
        NavMeshSurface nms;
        List<CivAI> civs = new List<CivAI>();
        float mapDimension;

        public void Initialize(float mapSize, int spawnAmount)
        {
            mapDimension = mapSize;

            navigation.Initialize();
            SpawnCivs(spawnAmount);
        }

        void SpawnCivs(int npcAmount)
        {
            foreach (var _ in Enumerable.Range(0, npcAmount))
            {
                var randomPoint = new Vector3(Random.Range(-mapDimension, mapDimension), 0,
                    Random.Range(-mapDimension, mapDimension));

                var civ = Instantiate(civPrefabs.PickOne(), npcContainer);
                civ.transform.position = randomPoint;
                civ.Initialize(navigation);
                civs.Add(civ);
            }
        }
        

        public void Remove(CivAI civ) => civs.Remove(civ);
        public CivAI GetRandomAlive() => civs.Where(c => c.Alive).PickOne();
    }
}
