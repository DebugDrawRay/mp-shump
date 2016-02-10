using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LoadoutContainer : MonoBehaviour
{
    public enum types
    {
        Ship,
        Primary,
        Secondary
    }
    public types containerType;

    public GameObject[] playerLoadoutObjects;
    private int currentIndex;
    public GameObject currentLoadoutObject;

    public Image icon;

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

        icon.sprite = playerLoadoutObjects[currentIndex].GetComponent<Icon>().icon;
        currentLoadoutObject = playerLoadoutObjects[currentIndex];

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
        currentIndex++;
        if(currentIndex >= playerLoadoutObjects.Length)
        {
            currentIndex = 0;
        }

        icon.sprite = playerLoadoutObjects[currentIndex].GetComponent<Icon>().icon;
        currentLoadoutObject = playerLoadoutObjects[currentIndex];
    }

    public void SelectPrevious()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = playerLoadoutObjects.Length - 1;
        }

        icon.sprite = playerLoadoutObjects[currentIndex].GetComponent<Icon>().icon;
        currentLoadoutObject = playerLoadoutObjects[currentIndex];
    }

    public GameObject GetSelectedObject()
    {
        return currentLoadoutObject;
    }
}
