using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public bool isEnemyAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.GetPath();
        StartCoroutine(FoolowPath(path)); // like invoke
       // print("Hi im back at start");
    }

    void OnPlayerDeath() // called by string refrence from other script
    {
        print("omg he died");
        isEnemyAlive = false;
    }

    IEnumerator FoolowPath(List<WayPoint> path) // IEnumerator LET THIS FUNCTION RUN ALONG SIDE OTHER THINGS
    {
        //print("Starting patrol");
        foreach (WayPoint wayPoint in path)
        {
            if(!isEnemyAlive) { yield break; }
            transform.position = wayPoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
        //print("Ending patrol");
    }

}
