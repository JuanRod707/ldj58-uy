using Assets.Scripts.Config;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Director
{
    public class SceneInitializer : MonoBehaviour
    {
        [SerializeField] private Character character;
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
            npcs.Initialize(config.MapSize, config.NPCCount);
            enemies.Initialize(config.MapSize, config.EnemiesCount);
            inputDirector.Initialize(config, character);
            character.Initialize(config, soulProvider, portals, inputDirector, enemies);
            soulProvider.Initialize(config, npcs);

            progress.Initialize(config);
            portals.Initialize();
            rifts.Initialize(config, progress);
        }
    }
}
