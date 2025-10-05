using System.Collections;
using Assets.Scripts.Config;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Director
{
    public class RiftProvider : MonoBehaviour
    {
        [SerializeField] Rift riftPrefab;
        
        Progress progress;
        float minRiftInterval, maxRiftInterval;
        float riftPullDistance;
        float riftIntervalCutPerRound;
        float mapSize;

        public void Initialize(GameplayConfig config, Progress progress)
        {
            this.progress = progress;
            minRiftInterval = config.MinRiftInterval;
            maxRiftInterval = config.MaxRiftInterval;
            riftIntervalCutPerRound = config.RiftIntervalCutPerRound;
            riftPullDistance = config.RiftPullDistance;
            mapSize = config.MapSize;

            StartCoroutine(WaitAndSpawnRift());
        }

        IEnumerator WaitAndSpawnRift()
        {
            var time = Random.Range(minRiftInterval, maxRiftInterval);
            time -= (time * riftIntervalCutPerRound * progress.CurrentRound);

            yield return new WaitForSeconds(time);

            var randomPoint = new Vector3(Random.Range(-mapSize, mapSize), 0,
                Random.Range(-mapSize, mapSize));

            var rift = Instantiate(riftPrefab, transform);
            rift.Initialize(riftPullDistance);
            rift.transform.position = randomPoint;

            StartCoroutine(WaitAndSpawnRift());
        }
    }
}
