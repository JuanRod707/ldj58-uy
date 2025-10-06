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
        bool respawn;

        public void Initialize(GameplayConfig config)
        {
            mapDimension = config.MapSize;
            respawn = config.NPCRespawn;
            navigation.Initialize();
            SpawnCivs(config.NPCCount);
        }

        void SpawnCivs(int npcAmount)
        {
            foreach (var _ in Enumerable.Range(0, npcAmount)) 
                SpawnCiv();
        }
        
        public CivAI GetRandomAlive() => civs.Where(c => c.Alive).PickOne();

        public void SpawnCiv()
        {
            var randomPoint = new Vector3(Random.Range(-mapDimension, mapDimension), 0,
                Random.Range(-mapDimension, mapDimension));

            var civ = Instantiate(civPrefabs.PickOne(), npcContainer);
            civ.transform.position = randomPoint;
            civ.Initialize(navigation, OnActorKilled);
            civs.Add(civ);
        }

        void OnActorKilled()
        {
            if(respawn)
                SpawnCiv();
        }
    }
}
