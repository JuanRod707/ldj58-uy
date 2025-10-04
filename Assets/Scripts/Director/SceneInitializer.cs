using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Director
{
    public class SceneInitializer : MonoBehaviour
    {
        [SerializeField] NPCInitializer npcs;
        [SerializeField] private Character player;
        [SerializeField] private SoulProvider soulProvider;
        [SerializeField] PortalProvider portals;
        void Start()
        {   
            npcs.Initialize();
            soulProvider.Initialize(npcs);
            player.Initialize(soulProvider, portals);
        }
    }
}
