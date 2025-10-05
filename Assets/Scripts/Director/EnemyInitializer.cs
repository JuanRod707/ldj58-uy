using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.NPCs;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Director
{
    public class EnemyInitializer : MonoBehaviour
    {
        [SerializeField] NavigationProvider navigation;
        [SerializeField] Transform npcContainer;
        [SerializeField] EnemyAI civPrefab;

        NavMeshData nm;
        NavMeshSurface nms;
        List<EnemyAI> enemies = new List<EnemyAI>();
        float mapDimension;

        public void Initialize(float mapSize, int spawnAmount)
        {
            mapDimension = mapSize;

            navigation.Initialize();
            SpawnEnemies(spawnAmount);
        }

        void SpawnEnemies(int npcAmount)
        {
            foreach (var _ in Enumerable.Range(0, npcAmount))
            {
                var randomPoint = new Vector3(Random.Range(-mapDimension, mapDimension), 0,
                    Random.Range(-mapDimension, mapDimension));

                var enemy = Instantiate(civPrefab, npcContainer);
                enemy.transform.position = randomPoint;
                enemy.Initialize(navigation);
                enemies.Add(enemy);
            }
        }

        public bool CheckIfAnyClose(Vector3 where, float maxDistance)
            => enemies.Any(c => Vector3.Distance(c.transform.position, where) < maxDistance);
        
        public EnemyAI GetClosestTo(Vector3 where, float maxDistance) =>
            enemies
                .Where(c => Vector3.Distance(c.transform.position, where) < maxDistance)
                .OrderBy(c => Vector3.Distance(c.transform.position, where))
                .FirstOrDefault();

        public void Remove(EnemyAI enemy) => enemies.Remove(enemy);
    }
}