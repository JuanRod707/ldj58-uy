using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [System.Serializable]
    public class PlayerStats
    {
        [SerializeField] private float speed;
        [SerializeField] private float speedReductionMult; 
        [SerializeField] private float detectionRadius;
        [SerializeField] private float detectionSpeed;

        private List<Soul> souls;
        public List<Soul> Souls
        {
            get
            {
                souls ??= new List<Soul>();

                return souls;
            }
            set => souls = value;
        }

        public float GetTotalSpeed => speed - (Souls.Count * speedReductionMult);
        public float DetectionRadius => detectionRadius;
        public float DetectionSpeed => detectionSpeed;
 
    }
}