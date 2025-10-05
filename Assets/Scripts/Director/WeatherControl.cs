using UnityEngine;

namespace Assets.Scripts.Director
{
    public class WeatherControl : MonoBehaviour
    {
        [SerializeField] ParticleSystem rainVfx;
        [SerializeField] ParticleSystem bloodRainVfx;
        [SerializeField] ParticleSystem dustStormVfx;

        [SerializeField] int rainStartRound;
        [SerializeField] int dustStartRound;
        [SerializeField] int bloodRainStartRound;

        public void OnRoundChanged(int round)
        {
            if(round == rainStartRound)
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
