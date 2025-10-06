using System;
using Assets.Scripts.Config;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Director
{
    public class Clock : MonoBehaviour
    {
        [SerializeField] TMP_Text label;

        float roundTime;
        float remaining;
        Action onTimeUp;

        public void Initialize(GameplayConfig config, Action onTimeUp)
        {
            this.onTimeUp = onTimeUp;
            roundTime = config.RoundTime;
            Restart();
        }

        void FixedUpdate()
        {
            remaining -= Time.fixedDeltaTime;
            var ts = TimeSpan.FromSeconds(remaining);
            label.text = ts.ToString(@"mm\:ss");

            if (remaining <= 0)
                onTimeUp();
        }

        public void Restart() => 
            remaining = roundTime;
    }
}
