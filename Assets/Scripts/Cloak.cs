using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloak : MonoBehaviour
{
    public float movementModifier, jumpModifier;
    public GameObject projectile;
    public int spriteNumber;
    
    public void fireProjectile(Vector3 startPos, int direction)
    {
        if (projectile != null && projectile.tag == "Projectile") {
            GameObject p = GameObject.Instantiate(projectile);
            p.transform.position = startPos;
            Projectile pScript = projectile.GetComponent<Projectile>();
            pScript.onProjectileCreation(direction);
        }

    }
}
