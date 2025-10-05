using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Director
{
    public class Clock : MonoBehaviour
    {
        [SerializeField] float roundTime;
        [SerializeField] TMP_Text label;

        float remaining;

        void Start()
        {
            remaining = roundTime;
        }

        void FixedUpdate()
        {
            remaining -= Time.fixedDeltaTime;
            var ts = TimeSpan.FromSeconds(remaining);
            label.text = ts.ToString(@"mm\:ss");
        }
    }
}
