using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeySpawner : MonoBehaviour
{
    [SerializeField] Transform EnemyParentTransform;

    [Range(0.2f, 3f)]
    [SerializeField] float secondsBetweenSpawns = 3f;
    [SerializeField] EnemyMovement enemy;
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
            newEnemy.transform.parent = EnemyParentTransform.transform;

        }
    }
}
