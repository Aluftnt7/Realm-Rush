using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeadEnemiesCounter : MonoBehaviour
{
    public static DeadEnemiesCounter instace;
    [SerializeField] Text deadEnemyCountText;

    public int deadEnemies;

    // Start is called before the first frame update
    void Start()
    {
        instace = this;
        deadEnemyCountText.text = deadEnemies.ToString();

    }

    public int UpdateDeadEnemyCount()
    {
        ++deadEnemies;
        deadEnemyCountText.text = deadEnemies.ToString();
        return 0;
        

    }
}
