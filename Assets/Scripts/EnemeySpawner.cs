using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeySpawner : MonoBehaviour
{
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
            Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }
}
