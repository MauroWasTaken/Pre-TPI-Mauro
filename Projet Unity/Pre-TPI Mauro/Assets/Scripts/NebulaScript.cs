using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// script qui gere le fond du jeu
/// </summary>
public class NebulaScript : MonoBehaviour
{
    [SerializeField]
    float speed;
    /// <summary>
    /// Fonction de base de unity lancée à chaque fois qu'il y a une nouvelle image
    /// cette fonction s'occupe de faire scroller le fond 
    /// </summary>
    void Update()
    {
        if (this.gameObject.transform.position.y <= -20.25)
        {
            this.gameObject.transform.position = new Vector3(0, this.gameObject.transform.position.y + 40.96f, 100);
        }
        this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0, -1 * speed * Time.deltaTime, 0);
    }
}
