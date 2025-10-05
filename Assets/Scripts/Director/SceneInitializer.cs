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
        [SerializeField] private SoulProvider soulProvider;
        [SerializeField] PortalProvider portals;
        [SerializeField] Progress progress;
        [SerializeField] RiftProvider rifts;

        [Header("PLAYER")]
        [SerializeField] RoamInput roamInput;
        [SerializeField] MinigameInput minigameInput;
        void Start()
        {   
            npcs.Initialize(config);
            soulProvider.Initialize(config, npcs);
            character.Initialize(config, soulProvider, portals);
            roamInput.Initialize(character);

            progress.Initialize(config);
            portals.Initialize();
            rifts.Initialize(config, progress);
        }
    }
}
