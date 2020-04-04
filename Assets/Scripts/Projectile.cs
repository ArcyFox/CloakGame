using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float velocity;

    public void onProjectileCreation(int leftRight) {
        if (leftRight == -1) { velocity *= -1; }
        


    }

    void FixedUpdate() {

        transform.position = new Vector3(transform.position.x + velocity, transform.position.y, transform.position.z);


    }

  
}
