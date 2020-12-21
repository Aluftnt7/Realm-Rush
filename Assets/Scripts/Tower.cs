using UnityEngine;
using System.Collections;
using System;

public class Tower : MonoBehaviour {
    //Parameters of each tower
    [SerializeField] Transform objectToPen;
    [SerializeField] float attackRange = 8f;
    [SerializeField] ParticleSystem projectileParticle;

    //State of each tower
    Transform targetEnemy;

    private void Update()
    {
        SetTargetEnemy();
        FireAtEnemy();
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if(sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosestEnemy(closestEnemy, testEnemy.transform);
        }

        targetEnemy = closestEnemy;



    }   

    private Transform GetClosestEnemy(Transform closestEnemy, Transform testEnemy)
    {
        float distanceToclosestoEnemy = Vector3.Distance(transform.position, closestEnemy.position);
        float distanceToclosestTestEnemy = Vector3.Distance(transform.position, testEnemy.position);
        return (distanceToclosestoEnemy < distanceToclosestTestEnemy ? closestEnemy : testEnemy);
    }

    private void FireAtEnemy()
    {
        if(targetEnemy)
        {
            objectToPen.LookAt(targetEnemy);
            HandleShoot();
        }
        else
        {
            Shoot(false);
        }
    }

    private void HandleShoot()
    {
          float distanceToEnemy =  Vector3.Distance(targetEnemy.transform.position,gameObject.transform.position);
          if(distanceToEnemy <= attackRange)
         {
            Shoot(true);
         }
          else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }
}



