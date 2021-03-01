using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 9)
        {
            EnemyScript[] enemyScripts = FindObjectsOfType<EnemyScript>();
            if (collision.gameObject.transform.position.x > 7.5 & collision.gameObject.GetComponent<EnemyScript>().MovementMultiplier == 1)
            {
                foreach(EnemyScript enemy in enemyScripts)
                {
                    enemy.Advance();
                }
            }
            else if (collision.gameObject.transform.position.x < -7.5 & collision.gameObject.GetComponent<EnemyScript>().MovementMultiplier == -1)
            {
                foreach (EnemyScript enemy in enemyScripts)
                {
                    enemy.Advance();
                }
            }
        }        
    }
}
