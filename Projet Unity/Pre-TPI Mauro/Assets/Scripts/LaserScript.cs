using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 20;
    private GameScript gameScript;
    // Update is called once per frame
    void Start()
    {
        gameScript = Object.FindObjectOfType<GameScript>();

    }
    void Update()
    {
        this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0, speed * Time.deltaTime, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 10)
        {
            
        }
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
