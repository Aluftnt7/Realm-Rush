using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool isEnemyAlive = true;
    [Header("Movement Stats")]
    [SerializeField] float movementPerFrame = 0.1f;
    [SerializeField] float waypointDwellTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.GetPath();
        StartCoroutine(FoolowPath(path)); // like invoke
    }

  

    IEnumerator FoolowPath(List<WayPoint> path) // IEnumerator LET THIS FUNCTION RUN ALONG SIDE OTHER THINGS
    {
        //print("Starting patrol");
        foreach (WayPoint wayPoint in path)
        {
            if(!isEnemyAlive) { yield break; }
            yield return StartCoroutine(MoveTowardsWaypoint(wayPoint)); // wait until enemy moves to next waypoint
            yield return new WaitForSeconds(waypointDwellTime); // dwell on a waypoint for a little while
        }
        //print("Ending patrol");
    }

    private IEnumerator MoveTowardsWaypoint(WayPoint wayPoint)
    {
        Vector3 fixedWaypointPos = new Vector3(wayPoint.transform.position.x, wayPoint.transform.position.y, wayPoint.transform.position.z);

        while (transform.position != fixedWaypointPos)
        {

            Vector3 pos = Vector3.MoveTowards(transform.position, fixedWaypointPos, movementPerFrame);
            transform.position = pos;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(fixedWaypointPos - transform.position), Time.deltaTime * 4);

            yield return null; // wait until next frame
        }

    }

    void OnPlayerDeath() // called by string refrence from other script
    {
        print("omg he died");
        isEnemyAlive = false;
    }

}
