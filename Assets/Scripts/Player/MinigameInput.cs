using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    public class MinigameInput : MonoBehaviour
    {
        private Battle battle;
        private PlayerInput input;
        private Character character;
        
        public void Initialize(PlayerInput input, Battle battle, Character character)
        {
            this.input = input;
            this.battle = battle;
            this.character = character;
        }

        public void Update()
        { 
            if (input.actions["Attack"].triggered)
            {
                battle.AttackEnemy();
            } 
        }
    }
}
