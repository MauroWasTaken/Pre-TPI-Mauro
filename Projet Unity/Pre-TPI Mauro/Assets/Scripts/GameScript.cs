using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    private int numberEnemies=0;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnEnemies(int rows)
    {
        for(int i = 0; i < rows; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                Instantiate(enemy, new Vector3(-7.5f+1.5f*j, 4f + (-3f * i), 0),transform.rotation);
                numberEnemies++;
            }
        }
    }
    public void EnemyKilled()
    {
        numberEnemies--;
    }
}
