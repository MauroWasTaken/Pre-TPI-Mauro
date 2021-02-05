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
    private Rigidbody2D rigidbody2D;
    [SerializeField]
    float Speed = 15;
    // Update is called once per frame
    private void Start()
    {
        rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rigidbody2D.position=rigidbody2D.position + new Vector2(Input.GetAxis("Horizontal") * Speed * Time.deltaTime,0);
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
            Instantiate(Lazer, this.gameObject.transform.position + new Vector3(0.006f,1.2f,0), this.gameObject.transform.rotation);
            fireCooldown = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
