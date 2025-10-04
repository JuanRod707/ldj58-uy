using System.Collections.Generic;
using Assets.Scripts.NPCs;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [System.Serializable]
    public class Stats
    {
        [SerializeField] private float speed;
        [SerializeField] private float speedReductionMult; 

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
 
    }
}