using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ProjectileValues
{
    public Vector2 _position;
    public Vector2 _direction;
    public float _movementSpeed;
    public float _travelLimit;
    public float _distanceTraveled;
    public uint _damage;
    public bool _isActive;
}

public class EnemyProjectile
{
    ProjectileValues _projectileValues = new ProjectileValues();
    public ProjectileValues GetProjectileValues { get { return _projectileValues; } }

    GameObject _projectileObject;

    public GameObject ProjectileObject { get { return _projectileObject; }  set { _projectileObject = value; } }
    public bool SetIsActive { set { _projectileValues._isActive = value; } }

    public void SetProjectileValues(ProjectileValues projectileValues)
    {
        _projectileValues._position = _projectileObject.transform.position;
        _projectileValues._direction = projectileValues._direction;
        _projectileValues._travelLimit = projectileValues._travelLimit;
        _projectileValues._movementSpeed = projectileValues._movementSpeed;
        _projectileValues._damage = projectileValues._damage;
        _projectileValues._isActive = projectileValues._isActive;
        _projectileValues._distanceTraveled = projectileValues._distanceTraveled;
    }

    public bool TraveledToFar(Vector2 direction)
    {
        _projectileValues._distanceTraveled += direction.magnitude * Time.deltaTime;
        if(_projectileValues._distanceTraveled >= _projectileValues._travelLimit)
        {
            return true;
        }
        return false;
    }
}
