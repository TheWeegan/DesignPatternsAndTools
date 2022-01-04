using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : EnemyFactory
{
    EnemyValues _enemyValues = new EnemyValues();
    
    ProjectileValues _projectileValues = new ProjectileValues();

    EnemyStateHandler _enemyState = new EnemyStateHandler();
    EnemyStateHandler.Event _currentEvent = EnemyStateHandler.Event.NotInRange;

    public void UpdateEnemy()
    {
        DecideEvent();
        
        if(_enemyValues._attackCooldown > 0)
        {
            _enemyValues._attackCooldown -= Time.deltaTime;
        }
        if(_enemyValues._attacking)
        {
            FireProjectile();
        }
        _enemyState.UpdateStateMachine(ref _currentEvent, ref _enemyValues);
    }

    void FireProjectile()
    {
        Vector2 userPosition = _enemyValues._gameObject.transform.position;
        Vector2 targetPosition = _enemyValues._targetGameObject.transform.position;
        _projectileValues._position = userPosition;
        _projectileValues._direction = CalculateDirection(userPosition, targetPosition);
        ProjectilePool.SpawnProjectile(_projectileValues);

        _enemyValues._attackCooldown = _enemyValues._attackCooldownTime;
        _enemyValues._attacking = false;
        _currentEvent = EnemyStateHandler.Event.Recharging;
    }
    Vector2 CalculateDirection(Vector2 user, Vector2 target)
    {
        return (target - user).normalized;
    }

    void DecideEvent()
    {
        if (PlayerInRange())
        {
            if (_currentEvent != EnemyStateHandler.Event.InRange 
                && _currentEvent != EnemyStateHandler.Event.Recharging)
            {
                _currentEvent = EnemyStateHandler.Event.InRange;
            }
        }
        else
        {
            if (_currentEvent != EnemyStateHandler.Event.NotInRange)
            {
                _currentEvent = EnemyStateHandler.Event.NotInRange;
            }
        }
    }

    bool PlayerInRange()
    {
        return ((_enemyValues._targetGameObject.transform.position - _enemyValues._gameObject.transform.position).magnitude) < _enemyValues._attackRange;
    }

    public void SetEnemyValues(EnemyValues enemyValues)
    {
        _enemyValues._gameObject = enemyValues._gameObject;
        _enemyValues._targetGameObject = enemyValues._targetGameObject;
        _enemyValues._movementSpeed = enemyValues._movementSpeed;
        _enemyValues._attackRange = enemyValues._attackRange;
        _enemyValues._attackCooldown = enemyValues._attackCooldown;
        _enemyValues._attackCooldownTime = enemyValues._attackCooldownTime;
        _enemyValues._attackTravelingDistance = enemyValues._attackTravelingDistance;
        _enemyValues._attackSpeed = enemyValues._attackSpeed;

        SetProjectileValues();
    }

    public void SetProjectileValues()
    {
        _projectileValues._direction = new Vector2(0, 0);
        _projectileValues._isActive = true;
        _projectileValues._movementSpeed = _enemyValues._attackSpeed;
        _projectileValues._travelLimit = _enemyValues._attackTravelingDistance;
        _projectileValues._distanceTraveled = 0;
    }
}
