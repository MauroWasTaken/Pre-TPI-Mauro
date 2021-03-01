using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    


    // Start is called before the first frame update
    void Start()
    {
        fireTiming = Random.Range(3f,30f);
        fireCooldown = 0;
        gameScript = Object.FindObjectOfType<GameScript>();
    }

    // Update is called once per frame 
    void Update()
    {
        if (fireCooldown >= fireTiming)
        {
            Instantiate(laserBall, this.gameObject.transform.position - new Vector3(0, 0.5f, 0), this.gameObject.transform.rotation);
            fireTiming = Random.Range(5f, 12f);
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            ContactPoint2D contactpoint2d = collision.GetContact(0);
            Destroy(this.gameObject);
            Instantiate(explosion, new Vector3(contactpoint2d.point.x, contactpoint2d.point.y, 0), transform.rotation);
            Destroy(collision.gameObject);
            gameScript.AddScore(100);
            gameScript.EnemyKilled();
        }
    }
    private void Move()
    {
        this.transform.position = this.transform.position + new Vector3(movementDistance* movementMultiplier,0,0);
    }
    public void Advance()
    {
        movementMultiplier = movementMultiplier * (-1);
        this.transform.position = this.transform.position + new Vector3(movementDistance * movementMultiplier, -0.5f, 0);

    }
    public int MovementMultiplier { get => movementMultiplier;}
}
