using Assets.Scripts.Config;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Director
{
    public class InputDirector : MonoBehaviour
    {
        [SerializeField] PlayerInput playerInput;
        [SerializeField] RoamInput roamInput;
        [SerializeField] MinigameInput minigameInput;
 
        private NPCInitializer enemyProvider;
        
        public void Initialize(GameplayConfig config, Character character)
        {
            roamInput.Initialize(character, playerInput);
            minigameInput.Initialize(playerInput, character.Battle, character);
            
            EnableRoam(true);
            EnableMinigame(false);
        }

        public void EnableRoam(bool enable) =>   roamInput.enabled = enable;

        public void EnableMinigame(bool enable)
        {
            minigameInput.enabled = enable; 
        }
        
    }
}