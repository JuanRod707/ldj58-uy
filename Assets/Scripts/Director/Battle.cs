using System;
using Assets.Scripts.Director;
using Assets.Scripts.NPCs;
using Assets.Scripts.Player;
using Assets.Scripts.UI;
using UnityEngine;

public class Battle : MonoBehaviour
{
    [SerializeField] BattlePanel panel;
    [SerializeField] float characterDamage;
    [SerializeField] float enemyDamage;
    
    private Character character;
    private EnemyAI enemy;
    private InputDirector inputDirector;
    private EnemyInitializer enemyProvider;

    private float battleProgress;
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
        battleProgress = 0.5f;
        enabled = true;
        character.Attacking(true);

        panel.Show();
        panel.UpdateBattle(battleProgress);
    }

    public void AttackEnemy()
    {
        if (!enemy) return;

        battleProgress += characterDamage;
        
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
            character.Stun(enemy.StunTime);
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
        if (currentEnemyAttackTime >= enemy.AttackTime)
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