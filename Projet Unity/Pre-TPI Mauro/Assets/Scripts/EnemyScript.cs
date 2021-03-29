using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// classe qui gere les vaisseaux ennemis
/// </summary>
public class EnemyScript : MonoBehaviour
{
    private float fireTiming;
    private float fireCooldown;
    [SerializeField]
    private GameObject laserBall;
    [SerializeField]
    private GameObject explosion;
    private GameScript gameScript;

    private BoxCollider2D triggerBox2D;
    private int movementMultiplier = -1;
    [SerializeField]
    private float movementTiming = 2;
    private float movementCooldown = 0;
    private float movementDistance = 0.1f;
    public bool isMad = false;

    public int MovementMultiplier { get => movementMultiplier; }

    /// <summary>
    /// fonction de base de unity qui est appelée quand l'objet player est instancié
    /// Celle ci instincie certaines variables
    /// </summary>
    void Start()
    {
        fireTiming = Random.Range(3f,30f);
        fireCooldown = 0;
        gameScript = Object.FindObjectOfType<GameScript>();
    }

    /// <summary>
    /// fonction de base de unity qui est appelée à chaque image
    /// elle gere les mouvements et tirs aleatoires 
    /// </summary>
    void Update()
    {
        if (fireCooldown >= fireTiming)
        {
            Instantiate(laserBall, this.gameObject.transform.position - new Vector3(0, 0.5f, 0), this.gameObject.transform.rotation);
            if (isMad)
            {
                fireTiming = Random.Range(3f, 9f);
            }
            else
            {
                fireTiming = Random.Range(5f, 12f);
            }
            gameScript.PlaySound(0);
            fireCooldown = 0;
        }
        if (movementCooldown>=movementTiming)
        {
            Move();
            movementCooldown = 0;
        }
        fireCooldown += Time.deltaTime;
        movementCooldown += Time.deltaTime;

    }
    /// <summary>
    /// fonction de base sur unity appelée quand il ya un object qui entre en collision avec le vaisseau
    /// check si c'est un laser du joueur et detruit le vaisseau en ajoutant des points
    /// </summary>
    /// <param name="collision">objet qui est entré en collision</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            ContactPoint2D contactpoint2d = collision.GetContact(0);
            Destroy(this.gameObject);
            Instantiate(explosion, new Vector3(contactpoint2d.point.x, contactpoint2d.point.y, 0), transform.rotation);
            EnemyScript[] enemies = GameObject.FindObjectsOfType<EnemyScript>();
            if (enemies.Length == 1)
            {
                gameScript.NextLevel = true;
            }
            Destroy(collision.gameObject);
            gameScript.AddScore(100);
            gameScript.PlaySound(3);
        }
    }
    /// <summary>
    /// Fonction permettant de deplacer le vaisseau
    /// </summary>
    private void Move()
    {
        this.transform.position = this.transform.position + new Vector3(movementDistance* movementMultiplier,0,0);
    }
    /// <summary>
    /// Fonction permettant d'avancer le vaisseau
    /// </summary>
    public void Advance()
    {
        movementMultiplier = movementMultiplier * (-1);
        this.transform.position = this.transform.position + new Vector3(movementDistance * movementMultiplier, -0.5f, 0);

    }

}
