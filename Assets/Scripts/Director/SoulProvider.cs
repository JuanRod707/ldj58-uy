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
        private List<Soul> availableSouls;
        
        public void Initialize(NPCInitializer npcInitializer)
        {
            this.npcInitializer = npcInitializer;
            availableSouls = new List<Soul>();
            SpawnSouls(npcInitializer.NPCAmount);
            StartCoroutine(KillCiviliansCoroutine());
        }

        private void SpawnSouls(int soulsAmount)
        {
            foreach (var _ in Enumerable.Range(0, soulsAmount))
            {
                Soul soul = Instantiate(soulPrefab, transform); 
                soul.gameObject.SetActive(false);
                availableSouls.Add(soul);
            }
        }
        
        public Soul GetClosestTo(Vector3 where) =>
            availableSouls
                .OrderBy(c => Vector3.Distance(c.transform.position, where))
                .First();

        public bool AnyInRange(Vector3 where, float range) => 
            availableSouls.Any(c => Vector3.Distance(c.transform.position, where) < range);

        public void RemoveSoul(Soul soulToRemove) => availableSouls.Remove(soulToRemove);
        
        private Soul GetAvailableSoul() => availableSouls.FirstOrDefault(x => !x.gameObject.activeSelf);
        private IEnumerator KillCiviliansCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetweenKills);

                CivAI civToKill = npcInitializer.GetRandomAlive();
                
                civToKill.Kill();
                
                Soul availableSoul = GetAvailableSoul();
                availableSoul.Summon();
                availableSoul.transform.position = civToKill.transform.position; 
            }
            
        }
    }
}