using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace Assets.Scripts.Player
{
    public class RoamInput : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        
        Character movement;
        SoulCollector soulCollector;

        public void FixedUpdate()
        {
            if (input.actions["Move"].IsPressed())
            {
                var inputVector = input.actions["Move"].ReadValue<Vector2>() *
                    (Time.fixedDeltaTime);
                movement.MovePlayer(inputVector);
            }

            if(input.actions["Attack"].IsInProgress())
                soulCollector.HoldAttack();
        }

        public void Initialize(Character character)
        {
            this.movement = character;
            this.soulCollector = character.GetComponent<SoulCollector>();
        }
    }
}
