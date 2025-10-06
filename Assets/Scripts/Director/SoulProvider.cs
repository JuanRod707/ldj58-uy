using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Config;
using Assets.Scripts.NPCs;
using UnityEngine; 

namespace Assets.Scripts.Director
{
    public class SoulProvider : MonoBehaviour
    {
        [SerializeField] private Soul soulPrefab;

        float minKillTime, maxKillTime;
        private NPCInitializer npcInitializer;
        private List<Soul> availableSouls = new();

        public void Initialize(GameplayConfig config, NPCInitializer npcInitializer)
        {
            minKillTime = config.MinTimePerKill;
            maxKillTime = config.MaxTimePerKill;

            this.npcInitializer = npcInitializer;
            StartCoroutine(KillCiviliansCoroutine());
        }
        
        
        public Soul GetClosestTo(Vector3 where) =>
            availableSouls
                .OrderBy(c => Vector3.Distance(c.transform.position, where))
                .First();

        public bool AnyInRange(Vector3 where, float range) => 
            availableSouls.Any(c => Vector3.Distance(c.transform.position, where) < range);

        public void RemoveSoul(Soul soulToRemove) => availableSouls.Remove(soulToRemove);
        
        private IEnumerator KillCiviliansCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minKillTime, maxKillTime));

                CivAI civToKill = npcInitializer.GetRandomAlive();
                
                civToKill.Kill();
                
                var soul = Instantiate(soulPrefab, transform);
                soul.Summon();
                soul.transform.position = civToKill.transform.position; 
                availableSouls.Add(soul);
            }
            
        }

        public bool Any() => 
            availableSouls.Any();
    }
}