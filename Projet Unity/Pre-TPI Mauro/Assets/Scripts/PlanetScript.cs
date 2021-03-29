using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// classe qui gere les planetes qui scrollent dans le fond
/// </summary>
public class PlanetScript : MonoBehaviour
{
    [SerializeField]
    float speed;
    /// <summary>
    /// Fonction de base de unity lancée à chaque fois qu'il y a une nouvelle image
    /// cette fonction s'occupe de faire scroller les planetes et les mettres à des entroits aleatoires
    /// </summary>
    void Update()
    {
        if (this.gameObject.transform.position.y <= -30)
        {
            this.gameObject.transform.position = new Vector3(Random.Range(-12f,12f),Random.Range(60f,125f),90);
        }
        this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0, -1 * speed * Time.deltaTime, 0);
    }
}
