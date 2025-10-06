using Assets.Scripts.Config;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Stats : MonoBehaviour
    {
        public float Speed => speed;
        public float SpeedCutPerSoul => speedCutPerSoul;
        public float CombatStrength => combatStrength;
        public float CaptureRate => captureRate;


        float speed;
        float speedCutPerSoul;
        float combatStrength;
        float captureRate;
        GameplayConfig config;

        public void Initialize(GameplayConfig config)
        {
            this.config = config;

            speed = config.GrimmySpeed;
            speedCutPerSoul = config.GrimmySpeedCutPerSoul;
            combatStrength = config.GrimmyStrength;
            captureRate = config.GrimmyCaptureRate;
        }

        public void UpgradeSpeed() => speed += speed * config.SpeedPerLvl;
        public void UpgradeCarryCap() => speedCutPerSoul -= speedCutPerSoul * config.CarryCapPerLvl;
        public void UpgradeStrength() => combatStrength += captureRate * config.StrPerLvl;
        public void UpgradeCapture() => captureRate += captureRate * config.CapturePerLvl;
    }
}