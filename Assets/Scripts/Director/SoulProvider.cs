using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.NPCs;
using UnityEngine; 

namespace Assets.Scripts.Director
{
    public class SoulProvider : MonoBehaviour
    {
        [SerializeField] private Soul soulPrefab; 
        [SerializeField] private float timeBetweenKills = 5f;
        
        private NPCInitializer npcInitializer;
        private List<Soul> availableSouls = new();

        public void Initialize(NPCInitializer npcInitializer)
        {
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
                yield return new WaitForSeconds(timeBetweenKills);

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