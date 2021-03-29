using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class ExplosionScript : MonoBehaviour
{
    [SerializeField]
    bool AnimationFinished = false;
    /// <summary>
    /// fonction de base de unity qui est appelée à chaque image
    /// son but est de supprimer le game object quand l'animation est terminée 
    /// </summary>
    void Update()
    {
        if (AnimationFinished)
        {
            Destroy(this.gameObject);
        }
    }
}
