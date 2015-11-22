using UnityEngine;
using System.Collections;

public class standardLaser : projectile
{
	void Update ()
    {
        rigid.velocity = direction * (speed + parentSpeed);
        extraUpdate();
	}
}
