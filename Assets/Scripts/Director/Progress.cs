using Assets.Scripts.Audio;
using Assets.Scripts.Config;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Director
{
    public class Progress : MonoBehaviour
    {
        [SerializeField] TMP_Text scoreLbl;
        [SerializeField] TMP_Text roundLbl;
        [SerializeField] GameObject roundSign;
        [SerializeField] Clock clock;

        [Header("Feedback")]
        [SerializeField] WeatherControl weather;
        [SerializeField] MusicPlayer musicPlayer;

        int baseSoulGoal;
        float incrementPerRound;
        int currentSoulGoal;
        int currentRound = 1;
        int collectedSouls = 0;
        public float CurrentRound => currentRound;

        public void Initialize(GameplayConfig config)
        {
            clock.Initialize(config);
            currentSoulGoal = config.BaseSoulGoal;
            incrementPerRound = config.GoalStretchPerRound;
            scoreLbl.text = $"{collectedSouls}/{currentSoulGoal}";
        }

        public void DeliverSoul()
        {
            collectedSouls++;
            scoreLbl.text = $"{collectedSouls}/{currentSoulGoal}";

            if (collectedSouls >= currentSoulGoal) 
                RoundUp();
        }

        void RoundUp()
        {
            currentRound++;
            currentSoulGoal += (int)(currentSoulGoal * incrementPerRound);

            collectedSouls = 0;
            scoreLbl.text = $"{collectedSouls}/{currentSoulGoal}";

            roundLbl.text = $"ROUND {currentRound}";
            roundSign.SetActive(false);
            roundSign.SetActive(true);

            clock.Restart();

            weather.OnRoundChanged(currentRound);
            musicPlayer.OnRoundChanged(currentRound);
        }
    }
}
