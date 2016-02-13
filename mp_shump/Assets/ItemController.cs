using UnityEngine;
using System.Collections;

public class ItemController : actionController
{
    public GameObject heldItem;

    public player assignedTarget;
    void Update()
    {
        UseHeldItem(input.item.WasPressed, heldItem.GetComponent<IUseableItem>());
    }

    void UseHeldItem(bool use, IUseableItem item)
    {
        if (use)
        {
            player target = GetComponent<player>().enemy;
            if (assignedTarget)
            {
                target = assignedTarget;
            }
            if (item != null && target)
            {
                item.useItem(target);
                heldItem = null;
            }
        }
    }
}
