using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBallScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 20;
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = this.gameObject.transform.position - new Vector3(0, speed * Time.deltaTime, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 11)
        {
            Destroy(this.gameObject);
        }
    }
}

