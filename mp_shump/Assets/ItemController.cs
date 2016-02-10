using UnityEngine;
using System.Collections;

public class ItemController : actionController
{
    [SerializeField]
    public GameObject heldItem;

    void Update()
    {
        if (input.item.WasPressed)
        {
            UseHeldItem(heldItem.GetComponent<IUseableItem>());
        }
    }

    void UseHeldItem(IUseableItem item)
    {
        if(item != null)
        {
            item.useItem();
        }
    }
}
