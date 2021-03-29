using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// classe qui gere les laser du joueur
/// </summary>
public class LaserScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 20;
    private GameScript gameScript;
    /// <summary>
    /// fonction de base de unity qui est appelée quand l'objet player est instancié
    /// Celle ci instincie certaines variables
    /// </summary>
    void Start()
    {
        gameScript = Object.FindObjectOfType<GameScript>();

    }
    /// <summary>
    /// fonction de base de unity qui est appelée à chaque image
    /// elle gere les mouvements
    /// </summary>
    void Update()
    {
        this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0, speed * Time.deltaTime, 0);
    }
    /// <summary>
    /// fonction de base sur unity appelée quand il ya un object qui entre en collision avec le laser
    /// check si il entre en contact avec un autre laser et incremente les points
    /// </summary>
    /// <param name="collision">objet qui est entré en collision</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 11:
                gameScript.AddScore(50);
                break;
            case 12:
                gameScript.ComboBreaker();
                break;
            default:
                return;

        }
        Destroy(this.gameObject);
    }
}
