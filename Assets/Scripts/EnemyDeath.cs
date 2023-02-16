using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject,0.3f);
        
    }
}
