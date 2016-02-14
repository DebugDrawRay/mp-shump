using UnityEngine;
using System.Collections;

public class itemPickup : MonoBehaviour
{
    [Header("Status")]
    public int lives;

    [Header("Weapon")]
    public int weaponLevelIncrease;

    [Header("Items")]
    public GameObject useableItem;

    void OnTriggerEnter2D(Collider2D hit)
    {
        player isPlayer = hit.GetComponent<player>();

        if (isPlayer)
        {
            status hasStatus = hit.GetComponent<status>();
            weaponController hasWeapons = hit.GetComponent<weaponController>();
            ItemController hasItems = hit.GetComponent<ItemController>();

            Animator hasAnim = hit.GetComponent<Animator>();
            if (hasStatus)
            {
                if (hasAnim)
                {
                    hasAnim.SetTrigger("pickup");
                }

                hasStatus.lives += lives;
            }

            if (hasWeapons)
            {
                hasWeapons.updateCurrentWeapons(weaponLevelIncrease);

                GetComponent<AudioSource>().enabled = true;

                if (hasAnim)
                {
                    hasAnim.SetTrigger("pickup");
                }
            }

            if (hasItems)
            {
                hasItems.AddHeldItem(useableItem);

                if (hasAnim)
                {
                    hasAnim.SetTrigger("pickup");
                }
            }

            if (hasItems || hasWeapons || hasStatus)
            {
                Destroy(gameObject);
            }
        }
    }
}
