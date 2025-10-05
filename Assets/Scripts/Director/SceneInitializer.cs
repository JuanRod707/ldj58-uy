using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Director
{
    public class SceneInitializer : MonoBehaviour
    {
        [SerializeField] private Character character;

        [Header("Directors")]
        [SerializeField] NPCInitializer npcs;
        [SerializeField] private SoulProvider soulProvider;
        [SerializeField] PortalProvider portals;

        [Header("PLAYER")]
        [SerializeField] RoamInput roamInput;
        [SerializeField] MinigameInput minigameInput;
        void Start()
        {   
            npcs.Initialize();
            soulProvider.Initialize(npcs);
            character.Initialize(soulProvider, portals);

            roamInput.Initialize(character);
        }
    }
}
