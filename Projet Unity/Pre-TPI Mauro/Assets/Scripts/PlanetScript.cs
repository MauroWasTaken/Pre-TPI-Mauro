using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    [SerializeField]
    float speed;
    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.y <= -30)
        {
            this.gameObject.transform.position = new Vector3(Random.Range(-12f,12f),Random.Range(60f,125f),90);
        }
        this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0, -1 * speed * Time.deltaTime, 0);
    }
}
