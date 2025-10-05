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

        public void Initialize(GameplayConfig config)
        {
            roundTime = config.RoundTime;
            Restart();
        }

        void FixedUpdate()
        {
            remaining -= Time.fixedDeltaTime;
            var ts = TimeSpan.FromSeconds(remaining);
            label.text = ts.ToString(@"mm\:ss");
        }

        public void Restart() => 
            remaining = roundTime;
    }
}
