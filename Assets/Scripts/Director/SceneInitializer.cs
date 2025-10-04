using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Director
{
    public class SceneInitializer : MonoBehaviour
    {
        [SerializeField] NPCInitializer npcs;
        [SerializeField] private PlayerCharacter player;
        void Start()
        {   
            player.Initialize();
            npcs.Initialize();
        }
    }
}
