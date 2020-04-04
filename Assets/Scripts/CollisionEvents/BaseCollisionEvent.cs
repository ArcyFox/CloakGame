using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollisionEvent : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);


        }


    }
}
