using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] WayPoint startWaypoint, endWayPoint;

    Dictionary<Vector2Int, WayPoint> grid = new Dictionary<Vector2Int, WayPoint>();
    Queue<WayPoint> queue = new Queue<WayPoint>();
    List<WayPoint> path = new List<WayPoint>();

    bool isRunning = true;
    WayPoint searchCenter; // the current searchCenter

    Vector2Int[] directions = // clockwise
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };


    public List<WayPoint> GetPath()
    {
        if(path.Count == 0)
        {
            CalculatePath();
        }
        return path;
        
    }

    private void CalculatePath()
    {
        LoadBlocks();
        BreadFirstSearch();
        CreatePath();
    }

    private void CreatePath()
    {
        SetAsPath(endWayPoint);
      
        WayPoint previos = endWayPoint.exploredFrom;
        while(previos != startWaypoint)
        {
            SetAsPath(previos);
            previos = previos.exploredFrom;
        }
        SetAsPath(startWaypoint);
        path.Reverse();
    }

    private void SetAsPath(WayPoint wayPoint)
    {
        path.Add(wayPoint);
        wayPoint.isPlaceable = false;
    }

    private void BreadFirstSearch()
    {
        queue.Enqueue(startWaypoint);
        while(queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            HaltIfEndFound();
            ExploreNeighbours();
        }
       // print("finish pathFinding?");
    }

    private void HaltIfEndFound()
    {
       if(searchCenter == endWayPoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if(!isRunning) { return; }
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoords = searchCenter.GetGridPos() + direction;
            if(grid.ContainsKey(neighbourCoords))
            {
                QueueNewNeighbours(neighbourCoords);

            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoords)
    {
        WayPoint neighbour = grid[neighbourCoords];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            //do nothing
        }
        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }
    }

  

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<WayPoint>();
        foreach (WayPoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if(grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping Overlapping block" + waypoint);
            }
            else
            {
                 grid.Add(gridPos, waypoint);
              
            }
        }
    }

    


}
