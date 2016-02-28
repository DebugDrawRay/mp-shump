using UnityEngine;
using System.Collections;

public class ItemController : actionController, IInformationBroadcast
{
    public PlayerInformationController targetInformationUi
    {
        get;
        set;
    }
    public GameObject heldItem;

    public player assignedTarget;

    void Update()
    {
        if (isActive)
        {
            UseHeldItem(input.item.WasPressed);
        }
    }

    void UseHeldItem(bool use)
    {
        if (use && heldItem)
        {
            IUseableItem item = heldItem.GetComponent<IUseableItem>();

            player target = GetComponent<player>().enemy;

            if (assignedTarget)
            {
                target = assignedTarget;
            }
            if (item != null && target)
            {
                item.useItem(target);
                heldItem.GetComponent<Icon>().icon = null;
                heldItem = null;
                if (targetInformationUi != null)
                {
                    targetInformationUi.SetItemIcon(null);
                }
            }
        }
    }

    public void AddHeldItem(GameObject item)
    {
        heldItem = item;
        if (targetInformationUi != null)
        {
            targetInformationUi.SetItemIcon(item.GetComponent<Icon>().icon);
        }
    }
}
