using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProjectilePool
{
    static List<EnemyProjectile> _projectilePool = new List<EnemyProjectile>();

    static Vector2 _poolPosition = new Vector2(0, -100);

    public static void CreateProjectiles(uint amount, ProjectileValues projectileValues, GameObject projectileObject)
    {
        for (uint i = 0; i < amount; i++)
        {
            EnemyProjectile projectile = new EnemyProjectile();
            projectile.ProjectileObject = GameObject.Instantiate(projectileObject);
            projectile.ProjectileObject.transform.position = _poolPosition;
            _projectilePool.Add(projectile);
        }
    }

    public static void ClearProjectilePool()
    {
        _projectilePool.Clear();
    }

    public static void SpawnProjectile(ProjectileValues projectileValues)
    {
        foreach (EnemyProjectile projectile in _projectilePool)
        {
            if (projectile.GetProjectileValues._isActive)
            {
                continue;
            }
            projectile.ProjectileObject.transform.position = projectileValues._position;
            projectile.SetProjectileValues(projectileValues);
            break;
        }
    }

    public static void UpdateProjectiles()
    {
        foreach (EnemyProjectile projectile in _projectilePool)
        {
            if (!projectile.GetProjectileValues._isActive)
            {
                continue;
            }
            UpdateMovement(projectile);
        }
    }

    static void UpdateMovement(EnemyProjectile projectile)
    {
        Vector2 direction = projectile.GetProjectileValues._direction;
        Vector2 position = projectile.ProjectileObject.transform.position;
        float movementSpeed = projectile.GetProjectileValues._movementSpeed;
        projectile.ProjectileObject.transform.position =
            position + (direction * movementSpeed * Time.deltaTime);
        if (projectile.TraveledToFar(direction))
        {
            projectile.SetIsActive = false;
            projectile.ProjectileObject.transform.position = _poolPosition;
        }
    }
}
