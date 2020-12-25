using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeySpawner : MonoBehaviour
{
    [Range(0.2f, 3f)]
    [SerializeField] float secondsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemy;
    [SerializeField] Transform EnemyParentTransform;

    [SerializeField] AudioClip spawnedEnemySfx;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNewEnemy(enemy));
    }


    IEnumerator SpawnNewEnemy(EnemyMovement enemy)
    {
        while (true) //forever
        {
            yield return new WaitForSeconds(secondsBetweenSpawns);
            var newEnemy =  Instantiate(enemy, transform.position, Quaternion.identity);
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySfx);
            newEnemy.transform.parent = EnemyParentTransform.transform;

        }
    }
}
