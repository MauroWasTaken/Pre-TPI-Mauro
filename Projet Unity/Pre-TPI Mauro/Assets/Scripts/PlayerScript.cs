using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private int fireRate = 1;
    private float fireCooldown=1;
    [SerializeField]
    private GameObject Lazer;
    private Rigidbody2D characterBody;
    private GameScript gameScript;
    [SerializeField]
    private float Speed = 15;
    [SerializeField]
    private GameObject explosion;
    public int playerId;
    // Update is called once per frame
    private void Start()
    {
        characterBody = this.gameObject.GetComponent<Rigidbody2D>();
        gameScript = Object.FindObjectOfType<GameScript>();
    }
    void Update()
    {
        characterBody.position= characterBody.position + new Vector2(Input.GetAxis("Horizontal"+GetPlayerID()) * Speed * Time.deltaTime,0);
        if (Input.GetButton("Fire"+GetPlayerID()))
        {
            Fire();
        }
        fireCooldown += Time.deltaTime;
    }
    void Fire()
    {
        if (!gameScript.IsPaused)
        {
            if (fireCooldown >= 1f / fireRate)
            {
                Instantiate(Lazer, this.gameObject.transform.position + new Vector3(0.006f, 0.5f, 2), this.gameObject.transform.rotation).gameObject.transform.localScale = new Vector3(0.7f,0.7f,1);
                fireCooldown = 0;
                gameScript.PlaySound(0);
            }
        }
    }
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
    private string GetPlayerID()
    {
        if(playerId == 0)
        {
            return "";
        }
        else
        {
            return "P" + playerId;  
        }
    }
}
