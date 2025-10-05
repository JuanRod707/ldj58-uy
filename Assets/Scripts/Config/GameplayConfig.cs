using UnityEngine;

namespace Assets.Scripts.Config
{
    [CreateAssetMenu(fileName = "Gameplay", menuName = "Config/Gameplay", order = 0)]
    public class GameplayConfig : ScriptableObject
    {
        [Header("Character")]
        public float GrimmySpeed;
        public float GrimmySpeedCutPerSoul;

        [Header("Round")]
        public int BaseSoulGoal;
        public float GoalStretchPerRound;
        public float RoundTime;

        [Header("NPCs")]
        public int NPCCount;
        public float MinTimePerKill;
        public float MaxTimePerKill;

        [Header("Entities")] 
        public float MinRiftInterval;
        public float MaxRiftInterval;
        public float RiftIntervalCutPerRound;
        public float RiftPullDistance;
        public float PortalPullDistance;

        [Header("Map")]
        public float MapSize;
    }
}
