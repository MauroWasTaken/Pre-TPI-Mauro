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
    private float speed;
    private float turningTime;
    private float turningCounter;
    private int directionMultiplier = -1;
    private GameScript gameScript;
    // Start is called before the first frame update
    void Start()
    {
        fireTiming = Random.Range(3f,10f);
        fireCooldown = 0;
        gameScript = Object.FindObjectOfType<GameScript>();
        turningTime = 1 / speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireCooldown >= fireTiming)
        {
            Instantiate(laserBall, this.gameObject.transform.position - new Vector3(0, 0.5f, 0), this.gameObject.transform.rotation);
            fireTiming = Random.Range(3f,10f);
            fireCooldown = 0;
        }
        if (turningCounter >= turningTime)
        {
            directionMultiplier = directionMultiplier * -1;
            turningCounter = 0;
        }
        this.gameObject.GetComponent<Rigidbody2D>().position = this.gameObject.GetComponent<Rigidbody2D>().transform.position + new Vector3(speed*directionMultiplier*Time.deltaTime,0);
        fireCooldown += Time.deltaTime;
        turningCounter += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            gameScript.AddScore(100);
            gameScript.EnemyKilled();
        }
    }
}
