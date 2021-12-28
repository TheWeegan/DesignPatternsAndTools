using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingEnemy : EnemyFactory
{
    EnemyValues _enemyValues = new EnemyValues();

    EnemyStateHandler _enemyState = new EnemyStateHandler();
    EnemyStateHandler.Event _currentEvent = EnemyStateHandler.Event.NotInRange;

    Vector2 _dashDirection = new Vector2(); 
    
    float _distanceTraveled = 0;

    public void UpdateEnemy()
    {
        DecideEvent();

        if (_enemyValues._attackCooldown > 0)
        {
            _enemyValues._attackCooldown -= Time.deltaTime;
        }
        else if (_enemyValues._attacking)
        {
            DashTowardsPlayer();
        }
        _enemyState.UpdateStateMachine(ref _currentEvent, ref _enemyValues);
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
    void DashTowardsPlayer()
    { 
        Vector2 userPosition = _enemyValues._gameObject.transform.position;
        if(_distanceTraveled <= 0)
        {
            Vector2 targetPosition = _enemyValues._targetGameObject.transform.position;
            _dashDirection = CalculateDirection(userPosition, targetPosition);            
        }

        userPosition += _dashDirection * _enemyValues._attackSpeed * Time.deltaTime;
        _distanceTraveled += _dashDirection.magnitude * Time.deltaTime;
        _enemyValues._gameObject.transform.position = userPosition;

        if (_distanceTraveled >= _enemyValues._attackTravelingDistance)
        {
            _enemyValues._attackCooldown = _enemyValues._attackCooldownTime;
            _enemyValues._attacking = false;
            _distanceTraveled = 0;
            _currentEvent = EnemyStateHandler.Event.Recharging;
        }
    }
    Vector2 CalculateDirection(Vector2 user, Vector2 target)
    {
        return (target - user).normalized;
    }

    bool PlayerInRange()
    {
        return ((_enemyValues._targetGameObject.transform.position - _enemyValues._gameObject.transform.position).magnitude) < _enemyValues._attackRange;
    }

    public void SetEnemyValue(EnemyValues enemyValues)
    {
        _enemyValues._gameObject = enemyValues._gameObject;
        _enemyValues._targetGameObject = enemyValues._targetGameObject;
        _enemyValues._movementSpeed = enemyValues._movementSpeed;
        _enemyValues._attackRange = enemyValues._attackRange;
        _enemyValues._attackCooldown = enemyValues._attackCooldown;
        _enemyValues._attackCooldownTime = enemyValues._attackCooldownTime;
        _enemyValues._attackSpeed = enemyValues._attackSpeed;
        _enemyValues._attackTravelingDistance = enemyValues._attackTravelingDistance;
    }
}
