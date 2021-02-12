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
    float Speed = 15;
    // Update is called once per frame
    private void Start()
    {
        characterBody = this.gameObject.GetComponent<Rigidbody2D>();
        gameScript = Object.FindObjectOfType<GameScript>();
    }
    void Update()
    {
        characterBody.position= characterBody.position + new Vector2(Input.GetAxis("Horizontal") * Speed * Time.deltaTime,0);
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
        fireCooldown += Time.deltaTime;
    }
    void Fire()
    {
        if (fireCooldown >= 1f/fireRate)
        {
            Instantiate(Lazer, this.gameObject.transform.position + new Vector3(0.006f,0.5f,2), this.gameObject.transform.rotation);
            fireCooldown = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11) 
        {
            gameScript.PlayerKilled();
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
