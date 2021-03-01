using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField]
    bool AnimationFinished = false;
    // Update is called once per frame
    void Update()
    {
        if (AnimationFinished)
        {
            Destroy(this.gameObject);
        }
    }
}
