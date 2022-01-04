using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _dashingEnemyPrefab;
    [SerializeField] GameObject _shootingEnemyPrefab;

    #region DashingEnemyValues
    [SerializeField] List<Vector2> _dashingEnemyPositions;
    [SerializeField] float _dashingEnemyAttackCooldownTime;
    [SerializeField] float _dashingEnemyAttackRange;
    [SerializeField] float _dashingEnemyAttackTravelingDistance;
    [SerializeField] float _dashingEnemyAttackSpeed;
    [SerializeField] float _dashingEnemyMovementSpeed;
    #endregion

    #region RangedEnemyValues
    [SerializeField] List<Vector2> _shootingEnemyPositions;
    [SerializeField] float _shootingEnemyAttackCooldownTime;
    [SerializeField] float _shootingEnemyAttackRange;
    [SerializeField] float _shootingEnemyAttackTravelingDistance;
    [SerializeField] float _shootingEnemyAttackSpeed;
    [SerializeField] float _shootingEnemyMovementSpeed;
    #endregion

    EnemyValues _enemyValues = new EnemyValues();
    EnemyFactory _enemyFactory = new EnemyFactory();

    List<DashingEnemy> _dashingEnemies = new List<DashingEnemy>();
    List<ShootingEnemy> _shootingEnemies = new List<ShootingEnemy>();

    void Start()
    {
        CreateEnemies();
    }

    void CreateEnemies()
    {
        CreateDashingEnemy();
        CreteShootingEnemies();
    }

    void Update()
    {
        foreach(DashingEnemy dashingEnemy in _dashingEnemies)
        {
            dashingEnemy.UpdateEnemy();
        }
        foreach (ShootingEnemy shootingEnemy in _shootingEnemies)
        {
            shootingEnemy.UpdateEnemy();
        }
    }


    void CreateDashingEnemy()
    {
        for (int i = 0; i < _dashingEnemyPositions.Count; i++)
        {
            _enemyValues._gameObject = GameObject.Instantiate(_dashingEnemyPrefab);
            _enemyValues._gameObject.transform.position = _dashingEnemyPositions[i];
            _enemyValues._targetGameObject = _player;

            _enemyValues._attacking = false;

            _enemyValues._attackRange = _dashingEnemyAttackRange;
            _enemyValues._attackCooldownTime = _dashingEnemyAttackCooldownTime;
            _enemyValues._attackTravelingDistance = _dashingEnemyAttackTravelingDistance;
            _enemyValues._attackSpeed = _dashingEnemyAttackSpeed;
            _enemyValues._movementSpeed = _dashingEnemyMovementSpeed;

            _enemyValues._attackCooldown = 0;
            _enemyFactory.CreateEnemy(this, EnemyType.Dashing, _enemyValues);
        }
    }
    void CreteShootingEnemies()
    {
        for (int i = 0; i < _shootingEnemyPositions.Count; i++)
        {
            _enemyValues._gameObject = GameObject.Instantiate(_shootingEnemyPrefab);
            _enemyValues._gameObject.transform.position = _shootingEnemyPositions[i];
            _enemyValues._targetGameObject = _player;

            _enemyValues._attacking = false;

            _enemyValues._attackRange = _shootingEnemyAttackRange;
            _enemyValues._attackCooldownTime = _shootingEnemyAttackCooldownTime;
            _enemyValues._attackTravelingDistance = _shootingEnemyAttackTravelingDistance;
            _enemyValues._attackSpeed = _shootingEnemyAttackSpeed;
            _enemyValues._movementSpeed = _shootingEnemyMovementSpeed;

            _enemyValues._attackCooldown = 0;
            _enemyFactory.CreateEnemy(this, EnemyType.Shooting, _enemyValues);
        }
    }

    public void AddEnemy(DashingEnemy dashingEnemy)
    {
        _dashingEnemies.Add(dashingEnemy);
    }
    public void AddEnemy(ShootingEnemy shootingEnemy)
    {
        _shootingEnemies.Add(shootingEnemy);
    }
}
