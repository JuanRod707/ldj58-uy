using System.Collections;
using Assets.Scripts.Config;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Director
{
    public class WeatherControl : MonoBehaviour
    {
        [SerializeField] ParticleSystem rainVfx;
        [SerializeField] ParticleSystem bloodRainVfx;
        [SerializeField] ParticleSystem dustStormVfx;
        [SerializeField] MeteorStrike meteorPrefab;

        [SerializeField] int rainStartRound;
        [SerializeField] int dustStartRound;
        [SerializeField] int bloodRainStartRound;
        [SerializeField] int meteorStrikeRound;
        
        [SerializeField] float minMeteorInterval, maxMeteorInterval;
        float mapSize;

        public void Initialize(GameplayConfig config) =>
            mapSize = config.MapSize;
        
        IEnumerator WaitAndThrowMeteorite()
        {
            yield return new WaitForSeconds(Random.Range(minMeteorInterval, maxMeteorInterval));
            var randomPos = new Vector3(Random.Range(-mapSize, mapSize), 0, Random.Range(-mapSize, mapSize));
            Instantiate(meteorPrefab, randomPos, Quaternion.identity);

            StartCoroutine(WaitAndThrowMeteorite());
        }


        public void OnRoundChanged(int round)
        {
            if (round == meteorStrikeRound)
                StartCoroutine(WaitAndThrowMeteorite());

            if (round == rainStartRound)
                rainVfx.Play();

            if (round == dustStartRound)
                dustStormVfx.Play();

            if (round == bloodRainStartRound)
            {
                rainVfx.Stop();
                bloodRainVfx.Play();
            }
        }
    }
}
