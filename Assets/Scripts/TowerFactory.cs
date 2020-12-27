using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] int towerLimit = 5;
    [SerializeField] Transform towerParentTransform;
    [SerializeField] Text TowerCount;
    [SerializeField] Text TowerCountText;


    Queue<Tower> towersQueue = new Queue<Tower>();


    private void Start()
    {
        TowerCount.text = (towerLimit -  towersQueue.Count).ToString();

    }

    public void AddTower(WayPoint baseWayPoint)
    {
        int numOfTowers = towersQueue.Count;
        if (numOfTowers < towerLimit)
        {
            InstantiatNewTower(baseWayPoint);
        }
        else
        {
            MoveExsitingTower(baseWayPoint);

        }
    }

    private void InstantiatNewTower(WayPoint baseWayPoint)
    {
        var newTower = Instantiate(towerPrefab, baseWayPoint.transform.position, Quaternion.identity);
        newTower.transform.parent = towerParentTransform.transform;
        baseWayPoint.isPlaceable = false;
        towersQueue.Enqueue(newTower);
        newTower.baseWayPoint = baseWayPoint;
        HandleTowerCountText();

    }

    private void HandleTowerCountText()
    {
        if(towerLimit - towersQueue.Count != 0)
        {
            TowerCount.text = (towerLimit - towersQueue.Count).ToString();
        }
        else
        {
            TowerCount.text = "";
            TowerCountText.text = "";
        }
    }

    private void MoveExsitingTower(WayPoint newBaseWayPoint)
    {
        var oldTower = towersQueue.Dequeue();
        oldTower.baseWayPoint.isPlaceable = true; // free up the block
        newBaseWayPoint.isPlaceable = false;
        oldTower.baseWayPoint = newBaseWayPoint;
        oldTower.transform.position = newBaseWayPoint.transform.position;
        towersQueue.Enqueue(oldTower);
        print("DATS IT BOY");
    }

  
}
