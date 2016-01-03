using UnityEngine;
using System.Collections;

public class standardLaser : projectile
{
	void Update ()
    {
        rigid.velocity = (transform.right * projectileSpeed);
        checkIfVisible();
	}
}
