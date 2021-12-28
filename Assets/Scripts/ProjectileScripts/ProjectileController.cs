using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] GameObject _projectileObject;
    
    ProjectileValues _projectileValues = new ProjectileValues();

    // Start is called before the first frame update
    void Start()
    {
        ProjectilePool.CreateProjectiles(500, _projectileValues, _projectileObject);
    }

    // Update is called once per frame
    void Update()
    {
        ProjectilePool.UpdateProjectiles();
    }
}
