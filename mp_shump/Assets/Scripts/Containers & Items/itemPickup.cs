using UnityEngine;
using System.Collections;

public class itemPickup : MonoBehaviour
{
    [Header("Status")]
    public int lives;

    [Header("Weapon")]
    public int weaponLevelIncrease;

    void OnTriggerEnter2D(Collider2D hit)
    {
        status hasStatus = hit.GetComponent<status>();
        weaponController hasWeapons = hit.GetComponent<weaponController>();
        Animator hasAnim = hit.GetComponent<Animator>();
        if (hasStatus)
        {
            hasStatus.lives += lives;
            Destroy(gameObject);
        }
        
        if(hasWeapons)
        {
            hasWeapons.updateCurrentWeapons(weaponLevelIncrease);

            GetComponent<AudioSource>().enabled = true;
            
            if(hasAnim)
            {
                hasAnim.SetTrigger("pickup");
            }

            Destroy(gameObject);
        }
    }
}
