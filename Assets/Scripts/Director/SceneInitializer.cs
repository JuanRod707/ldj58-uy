using Assets.Scripts.Config;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Director
{
    public class SceneInitializer : MonoBehaviour
    {
        [SerializeField] private Character character;
        [SerializeField] Stats stats;
        [SerializeField] GameplayConfig config;
        
        [Header("Directors")]
        [SerializeField] NPCInitializer npcs;
        [SerializeField] EnemyInitializer enemies;
        [SerializeField] private SoulProvider soulProvider;
        [SerializeField] PortalProvider portals;
        [SerializeField] Progress progress;
        [SerializeField] RiftProvider rifts;
        [SerializeField] InputDirector inputDirector;
        
        void Start()
        {   
            stats.Initialize(config);

            npcs.Initialize(config);
            enemies.Initialize(config);
            inputDirector.Initialize(config, character);
            character.Initialize(config, stats, soulProvider, portals, inputDirector, enemies);
            soulProvider.Initialize(config, npcs);

            progress.Initialize(config, stats);
            portals.Initialize();
            rifts.Initialize(config, progress);
        }
    }
}
