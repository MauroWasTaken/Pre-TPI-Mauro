using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// classe qui gere les laser des ennemis
/// </summary>
public class LaserBallScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 20;
    /// <summary>
    /// fonction de base de unity qui est appelée quand l'objet player est instancié
    /// Celle ci instincie certaines variables
    /// </summary>
    void Update()
    {
        this.gameObject.transform.position = this.gameObject.transform.position - new Vector3(0, speed * Time.deltaTime, 0);
    }
    /// <summary>
    /// fonction de base sur unity appelée quand il ya un object qui entre en collision avec le laser
    /// check si il entre en contact avec quelque chose et le detruit
    /// </summary>
    /// <param name="collision">objet qui est entré en collision</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 11)
        {
            Destroy(this.gameObject);
        }
    }
}

