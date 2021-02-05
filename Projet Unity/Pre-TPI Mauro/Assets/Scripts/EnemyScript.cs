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
    // Start is called before the first frame update
    void Start()
    {
        fireTiming = Random.Range(3f,10f);
        fireCooldown = 0;  
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
        this.gameObject.GetComponent<Rigidbody2D>().position = this.gameObject.GetComponent<Rigidbody2D>().transform.position - new Vector3(0, speed * Time.deltaTime / 10);
        fireCooldown += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            GameScript gameScript = Object.FindObjectOfType<GameScript>();
            gameScript.EnemyKilled();
        }
    }
}
