using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{       
    void OnCollisionEnter(Collision collision)
    {
         //Destroy bullet on collision
        Destroy(this.gameObject);  
    }
}
