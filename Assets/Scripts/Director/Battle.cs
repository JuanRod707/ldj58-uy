using System;
using Assets.Scripts.Director;
using Assets.Scripts.NPCs;
using Assets.Scripts.Player;
using UnityEngine;

public class Battle : MonoBehaviour
{
    [SerializeField] private int maxBattleEngage = 100;
    
    private Character character;
    private EnemyAI enemy;
    private InputDirector inputDirector;
    private EnemyInitializer enemyProvider;

    private float currentBattleEnage;
    private float currentEnemyAttackTime;
    
    public void Initialize(Character character, InputDirector inputDirector, EnemyInitializer enemyProvider)
    {
        this.enemyProvider = enemyProvider;
        this.character = character;
        this.inputDirector = inputDirector;
        enabled = false;
    }

    public void StartCombat()
    {
        inputDirector.EnableRoam(false);
        inputDirector.EnableMinigame(true);

        enemy = enemyProvider.GetClosestTo(transform.position, character.CollectDistance) as EnemyAI;
        enemy.EnterCombat(transform.position);
        
        currentEnemyAttackTime = enemy.AttackTime;
        currentBattleEnage = maxBattleEngage/2;
        enabled = true;
        character.Attacking(true);
    }

    public void AttackEnemy()
    {
        if (!enemy) return;

        currentBattleEnage += character.CombatDamage;
        
        if (currentBattleEnage >= maxBattleEngage)
        { 
            Debug.Log("Won Battle");
            enabled = false;
            
            enemyProvider.Remove(enemy);
            enemy.Kill(); 
            
            inputDirector.EnableMinigame(false);
            inputDirector.EnableRoam(true);
        }
    }

    private void Update()
    {
        if(!enemy) return;

        if (currentBattleEnage <= 0)
        {
            character.Stun(enemy.StunTime);
            inputDirector.EnableMinigame(false);
            enemy.Move();
            enemy = null;
            {
                Debug.Log("Enemy stunned");
            }
        }

        EnemyAttack();
    }

    private void EnemyAttack()
    {
        if (currentEnemyAttackTime >= enemy.AttackTime)
        {
            currentBattleEnage -= enemy.AttackAmount;
            currentEnemyAttackTime = 0;
        }
        else
        {
            currentEnemyAttackTime += Time.deltaTime;
        }
    }
}