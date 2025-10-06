using UnityEngine;

namespace Assets.Scripts.Config
{
    [CreateAssetMenu(fileName = "Gameplay", menuName = "Config/Gameplay", order = 0)]
    public class GameplayConfig : ScriptableObject
    {
        [Header("Character")]
        public float GrimmySpeed;
        public float GrimmySpeedCutPerSoul;
        public float GrimmyCaptureRate;

        [Header("Round")]
        public int BaseSoulGoal;
        public float GoalStretchPerRound;
        public float RoundTime;

        [Header("NPCs")] 
        public bool NPCRespawn;
        public int NPCCount;
        public float MinTimePerKill;
        public float MaxTimePerKill;

        [Header("Enemies")]
        public bool EnemyRespawn;
        public int EnemiesCount;

        [Header("Entities")] 
        public float MinRiftInterval;
        public float MaxRiftInterval;
        public float RiftIntervalCutPerRound;
        public float RiftPullDistance;
        public float PortalPullDistance;

        [Header("Map")]
        public float MapSize;

        [Header("Combat")] 
        public float GrimmyStrength;
        public float EnemyStrength;
        public float EnemyAttackRate;
        public float StunPenaltyTime;

        [Header("Upgrades")] 
        public float SpeedPerLvl;
        public float CarryCapPerLvl;
        public float StrPerLvl;
        public float CapturePerLvl;
    }
}
