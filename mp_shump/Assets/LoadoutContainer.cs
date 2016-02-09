using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LoadoutContainer : MonoBehaviour
{
    public Image icon;

    public Sprite[] playerLoadoutObjects;
    private int currentObject;

    private Image render;

    public Color selectedColor;
    private Color baseColor;
    private bool isSelected;

    public PlayerActions input;

    private bool directionHeld;
    void Awake()
    {
        render = GetComponent<Image>();
        baseColor = render.color;
    }

    void Update()
    {
        if (isSelected)
        {
            if (input.move.Y >= 0.75 && !directionHeld)
            {
                SelectNext();
                directionHeld = true;
            }
            if (input.move.Y <= -0.75 && !directionHeld)
            {
                SelectPrevious();
                directionHeld = true;
            }

            if (input.move.Y > -0.75 && input.move.Y < 0.75)
            {
                directionHeld = false;
            }
        }
    }

    public void Select()
    {
        render.color = selectedColor;
        isSelected = true;
    }

    public void Deselect()
    {
        render.color = baseColor;
        isSelected = false;
    }

    public void SelectNext()
    {
        currentObject++;
        if(currentObject >= playerLoadoutObjects.Length)
        {
            currentObject = 0;
        }

        icon.sprite = playerLoadoutObjects[currentObject];
    }

    public void SelectPrevious()
    {
        currentObject--;
        if (currentObject < 0)
        {
            currentObject = playerLoadoutObjects.Length - 1;
        }

        icon.sprite = playerLoadoutObjects[currentObject];
    }

}
