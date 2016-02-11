using UnityEngine;
using System.Collections;

public class ItemController : actionController
{
    public GameObject heldItem;

    public player assignedTarget;
    void Update()
    {
        if (input.item.WasPressed)
        {
            UseHeldItem(heldItem.GetComponent<IUseableItem>());
        }
    }

    void UseHeldItem(IUseableItem item)
    {
        player target = GetComponent<player>().enemy;
        if(assignedTarget)
        {
            target = assignedTarget;
        }
        if(item != null && target)
        {
            item.useItem(target);
        }
    }
}
