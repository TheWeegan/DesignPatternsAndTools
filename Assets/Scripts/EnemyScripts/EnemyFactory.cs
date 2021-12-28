using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Dashing,
    Ranged,
    Count
}

public struct EnemyValues
{
    public GameObject _gameObject;
    public GameObject _targetGameObject;

    public bool _attacking;

    public float _movementSpeed;
    public float _attackRange;
    public float _attackCooldown;
    public float _attackCooldownTime;
    public float _attackTravelingDistance;
    public float _attackSpeed;
}

public class EnemyFactory
{   
    public void CreateEnemy(EnemyController enemyController, EnemyType enemyType, EnemyValues enemyValues)
    {
        switch (enemyType)
        {
            case EnemyType.Dashing:
                DashingEnemy newMeleeEnemy = new DashingEnemy();
                newMeleeEnemy.SetEnemyValue(enemyValues);
                enemyController.AddEnemy(newMeleeEnemy);
                break;
            case EnemyType.Ranged:
                RangedEnemy newRangedEnemy = new RangedEnemy();
                newRangedEnemy.SetEnemyValues(enemyValues);
                enemyController.AddEnemy(newRangedEnemy);
                break;
            default:
                break;
        }
    }
}
