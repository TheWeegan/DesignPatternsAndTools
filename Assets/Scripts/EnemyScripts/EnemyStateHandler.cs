using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateHandler
{
    public enum State
    {
        Walking,
        Idle, 
        Attacking,
        Count
    }

    public enum Event
    {
        InRange,
        NotInRange,
        Recharging,
        Count
    }

    State _state = State.Idle;

    public void UpdateStateMachine(ref Event newEvent, ref EnemyValues enemyValues)
    {
        switch (newEvent)
        {
            case Event.Recharging:
                switch (_state)
                {
                    case State.Attacking:
                        _state = State.Idle;
                        break;
                    case State.Idle:
                        if (enemyValues._attackCooldown <= 0)
                        {
                            _state = State.Walking;
                        }
                        break;
                    case State.Walking:
                        newEvent = Event.NotInRange;
                        break;
                }
                break;
            case Event.InRange:
                switch (_state)
                {
                    case State.Attacking:
                        enemyValues._attacking = true;
                        break;

                    case State.Idle:
                        if (enemyValues._attackCooldown <= 0)
                        {
                            _state = State.Attacking;
                        }
                        else
                        {
                            _state = State.Idle;
                            newEvent = Event.Recharging;
                        }
                        break;

                    case State.Walking:
                        _state = State.Idle;
                        break;
                }
                break;
            case Event.NotInRange:
                switch (_state)
                {
                    case State.Idle:
                        _state = State.Walking;
                        break;

                    case State.Walking:
                        WalkTowardsPlayer(enemyValues);
                        break;
                }
                break;
            default:
                break;

        }
    }

    void WalkTowardsPlayer(EnemyValues enemyValues)
    {
        Vector2 direction = enemyValues._targetGameObject.transform.position - 
            enemyValues._gameObject.transform.position;
        direction.Normalize();
        Vector2 newPosition = (Vector2)enemyValues._gameObject.transform.position + (direction * enemyValues._movementSpeed * Time.deltaTime);
        enemyValues._gameObject.transform.position = newPosition;
    }
}
