using System;
using Assets.Scripts.Config;
using Assets.Scripts.Director;
using Assets.Scripts.NPCs;
using Assets.Scripts.Player;
using Assets.Scripts.UI;
using UnityEngine;

public class Battle : MonoBehaviour
{
    [SerializeField] BattlePanel panel;
    
    private Character character;
    private EnemyAI enemy;
    private InputDirector inputDirector;
    private EnemyInitializer enemyProvider;

    private float battleProgress;
    private float currentEnemyAttackTime;
    float enemyDamage;
    float enemyAttackRate;
    float stunPenaltyTime;

    Stats stats;
    
    public void Initialize(Character character, GameplayConfig config, Stats stats, InputDirector inputDirector, EnemyInitializer enemyProvider)
    {
        this.enemyProvider = enemyProvider;
        this.character = character;
        this.inputDirector = inputDirector;
        this.stats = stats;

        enemyDamage = config.EnemyStrength;
        enemyAttackRate = config.EnemyAttackRate;
        stunPenaltyTime = config.StunPenaltyTime;


        enabled = false;
    }

    public void StartCombat()
    {
        inputDirector.EnableRoam(false);
        inputDirector.EnableMinigame(true);

        enemy = enemyProvider.GetClosestTo(transform.position, character.CollectDistance) as EnemyAI;
        enemy.EnterCombat(transform.position);
        
        currentEnemyAttackTime = enemyAttackRate;
        battleProgress = 0.5f;
        enabled = true;
        character.Attacking(true);

        panel.Show();
        panel.UpdateBattle(battleProgress);
    }

    public void AttackEnemy()
    {
        if (!enemy) return;

        battleProgress += stats.CombatStrength;
        
        if (battleProgress >= 1)
        { 
            enabled = false;
            
            enemyProvider.Remove(enemy);
            enemy.Kill(); 
            
            inputDirector.EnableMinigame(false);
            inputDirector.EnableRoam(true);
            panel.Hide();
        }
    }

    private void Update()
    {
        if (!enemy) return;

        if (battleProgress <= 0)
        {
            character.Stun(stunPenaltyTime);
            inputDirector.EnableMinigame(false);
            enemy.Move();
            enemy = null;
            panel.Hide();
        }

        panel.UpdateBattle(battleProgress);

        if (enemy)
            EnemyAttack();
    }

    private void EnemyAttack()
    {
        if (currentEnemyAttackTime >= enemyAttackRate)
        {
            battleProgress -= enemyDamage;
            currentEnemyAttackTime = 0;
        }
        else
        {
            currentEnemyAttackTime += Time.deltaTime;
        }
    }
}