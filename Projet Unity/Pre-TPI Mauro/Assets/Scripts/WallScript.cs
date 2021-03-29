using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// gere les mur invisibles à côté du plan de jeu
/// </summary>
public class WallScript : MonoBehaviour
{
    /// <summary>
    /// Fonction lancé quand un object entre dans sa collision
    /// En fonction de ce qui entre soit il detruit le projectil ou il fait avancer les aliens
    /// </summary>
    /// <param name="collision">entité qui est entrée en colision avec le mur</param>
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
