using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  WayPoint : MonoBehaviour
{
    // ok to make it public because its a data class
    public bool isExplored = false;
    public WayPoint exploredFrom;
    public bool isPlaceable = true;
    bool isFirstRun = true;

    const int gridSize = 10;
    Vector2Int gridPos;
    
   
    public Vector2Int GetGridPos()
    {
        return new Vector2Int
            (
                  Mathf.RoundToInt(transform.position.x / gridSize),
                  Mathf.RoundToInt(transform.position.z / gridSize)
            );
    }

    private void Update()
    {
        if (isFirstRun)
        {
            HandleCanonBaseAdd();
            isFirstRun = false;
            print("only once");
        }
    }

    void HandleCanonBaseAdd()
    {
        if (isPlaceable)
        {
            Transform canonBase =  transform.Find("Canon Base");
            canonBase.GetComponent<Renderer>().enabled = true;
        }
    }



    public int GetGridSize()
       {
        return gridSize;
       }


   
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        if (Input.GetMouseButtonDown(0))
            if(isPlaceable)
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
            }
            else
            {
                print("cannot place here");
            }
    
    }



}
