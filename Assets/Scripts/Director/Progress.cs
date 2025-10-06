using Assets.Scripts.Audio;
using Assets.Scripts.Config;
using Assets.Scripts.UI;
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

        [Header("Panels")] 
        [SerializeField] EndPanel endPanel;

        int baseSoulGoal;
        float incrementPerRound;
        int currentSoulGoal;
        int currentRound = 1;
        int collectedSouls = 0;

        int totalSouls;

        public float CurrentRound => currentRound;

        public void Initialize(GameplayConfig config)
        {
            clock.Initialize(config, OnTimeUp);
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

            totalSouls++;
        }

        void RoundUp()
        {
            currentRound++;
            currentSoulGoal += (int)(currentSoulGoal * incrementPerRound);

            collectedSouls = 0;
            scoreLbl.text = $"{collectedSouls}/{currentSoulGoal}";

            roundLbl.text = $"PHASE {currentRound}";
            roundSign.SetActive(false);
            roundSign.SetActive(true);

            clock.Restart();

            weather.OnRoundChanged(currentRound);
            musicPlayer.OnRoundChanged(currentRound);
        }

        void OnTimeUp()
        {
            clock.enabled = false;
            endPanel.Show(totalSouls, currentRound);
        }
    }
}
