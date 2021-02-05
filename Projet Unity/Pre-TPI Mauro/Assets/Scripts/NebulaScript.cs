using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebulaScript : MonoBehaviour
{
    [SerializeField]
    float speed;
    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.y <= -20.25)
        {
            this.gameObject.transform.position = new Vector3(0, this.gameObject.transform.position.y + 40.96f, 100);
        }
        this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0, -1 * speed * Time.deltaTime, 0);
    }
}
