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
            StartCoroutine(WaitAndKill());
        }
        
        
        public Soul GetClosestTo(Vector3 where) =>
            availableSouls
                .OrderBy(c => Vector3.Distance(c.transform.position, where))
                .First();

        public bool AnyInRange(Vector3 where, float range) => 
            availableSouls.Any(c => Vector3.Distance(c.transform.position, where) < range);

        public void RemoveSoul(Soul soulToRemove) => availableSouls.Remove(soulToRemove);

        private IEnumerator WaitAndKill()
        {
            yield return new WaitForSeconds(Random.Range(minKillTime, maxKillTime));

            CivAI civToKill = npcInitializer.GetRandomAlive();
            Kill(civToKill);
            StartCoroutine(WaitAndKill());
        }

        void Kill(CivAI candidate)
        {
            candidate.Kill();
            var soul = Instantiate(soulPrefab, transform);
            soul.Summon();
            soul.transform.position = candidate.transform.position;
            availableSouls.Add(soul);
        }

        public bool Any() => 
            availableSouls.Any();

        public void KillInZone(Vector3 where, float radius)
        {
            var candidates = npcInitializer.GetAllInZone(where, radius);
            foreach (var c in candidates.ToList())
                Kill(c);
        }
    }
}