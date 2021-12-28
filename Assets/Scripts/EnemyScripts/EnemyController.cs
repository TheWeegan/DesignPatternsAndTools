using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _dashingEnemyPrefab;
    [SerializeField] GameObject _rangedEnemyPrefab;

    #region DashingEnemyValues
    [SerializeField] List<Vector2> _dashingEnemyPositions;
    [SerializeField] float _dashingEnemyAttackCooldownTime;
    [SerializeField] float _dashingEnemyAttackRange;
    [SerializeField] float _dashingEnemyAttackTravelingDistance;
    [SerializeField] float _dashingEnemyAttackSpeed;
    [SerializeField] float _dashingEnemyMovementSpeed;
    #endregion

    #region RangedEnemyValues
    [SerializeField] List<Vector2> _rangedEnemyPositions;
    [SerializeField] float _rangedEnemyAttackCooldownTime;
    [SerializeField] float _rangedEnemyAttackRange;
    [SerializeField] float _rangedEnemyAttackTravelingDistance;
    [SerializeField] float _rangedEnemyAttackSpeed;
    [SerializeField] float _rangedEnemyMovementSpeed;
    #endregion

    EnemyValues _enemyValues = new EnemyValues();
    EnemyFactory _enemyFactory = new EnemyFactory();

    List<DashingEnemy> _dashingEnemies = new List<DashingEnemy>();
    List<RangedEnemy> _rangedEnemies = new List<RangedEnemy>();

    void Start()
    {
        CreateEnemies();
    }

    void CreateEnemies()
    {
        CreateMeleeEnemies();
        CreteRangedEnemies();
    }

    void Update()
    {
        foreach(DashingEnemy enemyMelee in _dashingEnemies)
        {
            enemyMelee.UpdateEnemy();
        }
        foreach (RangedEnemy enemyRanged in _rangedEnemies)
        {
            enemyRanged.UpdateEnemy();
        }
    }


    void CreateMeleeEnemies()
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
    void CreteRangedEnemies()
    {
        for (int i = 0; i < _rangedEnemyPositions.Count; i++)
        {
            _enemyValues._gameObject = GameObject.Instantiate(_rangedEnemyPrefab);
            _enemyValues._gameObject.transform.position = _rangedEnemyPositions[i];
            _enemyValues._targetGameObject = _player;

            _enemyValues._attacking = false;

            _enemyValues._attackRange =_rangedEnemyAttackRange;
            _enemyValues._attackCooldownTime = _rangedEnemyAttackCooldownTime;
            _enemyValues._attackTravelingDistance = _rangedEnemyAttackTravelingDistance;
            _enemyValues._attackSpeed = _rangedEnemyAttackSpeed;
            _enemyValues._movementSpeed = _rangedEnemyMovementSpeed;

            _enemyValues._attackCooldown = 0;
            _enemyFactory.CreateEnemy(this, EnemyType.Ranged, _enemyValues);
        }
    }

    public void AddEnemy(DashingEnemy  enemyMelee)
    {
        _dashingEnemies.Add(enemyMelee);
    }
    public void AddEnemy(RangedEnemy enemyRanged)
    {
        _rangedEnemies.Add(enemyRanged);
    }
}
