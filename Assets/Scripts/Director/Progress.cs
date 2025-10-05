using TMPro;
using UnityEngine;

namespace Assets.Scripts.Director
{
    public class Progress : MonoBehaviour
    {
        [SerializeField] int baseSoulGoal;
        [SerializeField] float incrementPerRound;
        [SerializeField] TMP_Text scoreLbl;
        [SerializeField] TMP_Text roundLbl;
        [SerializeField] GameObject roundSign;

        int currentSoulGoal;
        int currentRound = 1;
        int collectedSouls = 0;

        public void Initialize() => 
            currentSoulGoal = baseSoulGoal;

        public void DeliverSoul()
        {
            collectedSouls++;
            scoreLbl.text = collectedSouls.ToString();

            if (collectedSouls >= currentSoulGoal) 
                RoundUp();
        }

        void RoundUp()
        {
            currentRound++;
            currentSoulGoal += (int)(currentSoulGoal * incrementPerRound);

            collectedSouls = 0;
            scoreLbl.text = collectedSouls.ToString();

            roundLbl.text = $"ROUND {currentRound}";
            roundSign.SetActive(false);
            roundSign.SetActive(true);
        }
    }
}
