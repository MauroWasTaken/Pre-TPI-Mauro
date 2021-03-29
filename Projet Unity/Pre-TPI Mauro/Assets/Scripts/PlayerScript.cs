using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// classe qui gere les vaisseaux controlés par les joueurs
/// </summary>
public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float fireRate = 1;
    private float fireCooldown = 1;
    [SerializeField]
    private GameObject Lazer;
    private Rigidbody2D characterBody;
    private GameScript gameScript;
    [SerializeField]
    private float Speed = 15;
    [SerializeField]
    private GameObject explosion;
    public int playerId;
    /// <summary>
    /// fonction de base de unity qui est appelée quand l'objet player est instancié
    /// </summary>
    private void Start()
    {
        characterBody = this.gameObject.GetComponent<Rigidbody2D>();
        gameScript = Object.FindObjectOfType<GameScript>();
    }
    /// <summary>
    /// fonction de base de unity qui est appelée à chaque image
    /// elle gere les mouvements et tirs 
    /// </summary>
    void Update()
    {
        characterBody.position = characterBody.position + new Vector2(Input.GetAxis("Horizontal" + GetPlayerID()) * Speed * Time.deltaTime, 0);
        if (Input.GetButton("Fire" + GetPlayerID()))
        {
            Fire();
        }
        fireCooldown += Time.deltaTime;
    }
    /// <summary>
    /// fonction qui gere le fait de tirer des lasers
    /// </summary>
    void Fire()
    {
        if (!gameScript.IsPaused)
        {
            if (fireCooldown >= 1f / fireRate)
            {
                float laserWidth = 0.7f;
                float laserHeight = 0.7f;
                switch (gameScript.laserType)
                {
                    case 0:
                        laserWidth = 0.7f;
                        laserHeight = 0.7f;
                        fireRate = 1;
                        break;
                    case 1:
                        laserWidth = 2f;
                        laserHeight = 1f;
                        fireRate = 0.75f;
                        break;
                    case 2:
                        laserWidth = 0.3f;
                        laserHeight = 0.3f;
                        fireRate = 5;
                        break;
                }

                Instantiate(Lazer, this.gameObject.transform.position + new Vector3(0.006f, 0.5f, 2), this.gameObject.transform.rotation).gameObject.transform.localScale = new Vector3(laserWidth, laserHeight, 1);
                fireCooldown = 0;
                gameScript.PlaySound(0);
            }
        }
    }
    /// <summary>
    /// fonction de base sur unity appelée quand il ya un object qui entre en collision avec le joueur
    /// check si c'est un laser enemy et detruit le vaisseau
    /// </summary>
    /// <param name="collision">objet qui est entré en collision</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            ContactPoint2D contactpoint2d = collision.GetContact(0);
            Instantiate(explosion, new Vector3(contactpoint2d.point.x, contactpoint2d.point.y, 0), transform.rotation);
            gameScript.PlayerKilled(playerId);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
    /// <summary>
    /// fonction utilisée pour savoir quels inputs sont utilisés pour le vaiseau
    /// </summary>
    /// <returns>""si c'est le mode solo et "P(id joueur)" si c'est en coop</returns>
    private string GetPlayerID()
    {
        if (playerId == 0)
        {
            return "";
        }
        else
        {
            return "P" + playerId;
        }
    }
}
