using UnityEngine;
using System.Collections;

public class SendEnemiesItem : MonoBehaviour, IUseableItem
{
    public void useItem()
    {
        Debug.Log("Item Used");
    }
}
