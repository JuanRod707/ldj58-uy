using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace Assets.Scripts.Player
{
    public class RoamInput : MonoBehaviour
    {
        [SerializeField] PausePanel pause;
 
        private PlayerInput input;

        Character character;
        SoulCollector soulCollector;

        public void FixedUpdate()
        {
            if (input.actions["Move"].IsPressed())
            {
                var inputVector = input.actions["Move"].ReadValue<Vector2>() *
                    (Time.fixedDeltaTime);
                character.MovePlayer(inputVector);
            }
            else
                character.Stop();

            if (input.actions["Attack"].IsInProgress())
            {
                soulCollector.HoldAttack();
                character.Attacking(true);
            }

            if (input.actions["Attack"].WasReleasedThisFrame()) 
                character.Attacking(false);

            if (input.actions["Pause"].IsPressed()) 
                pause.Open();
        }

        public void Initialize(Character character, PlayerInput input)
        {
            this.input = input;
            this.character = character;
            this.soulCollector = character.GetComponent<SoulCollector>();
        }
    }
}
