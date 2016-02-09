using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectableContainer : MonoBehaviour
{
    public LoadoutContainer[] selectors;
    private int currentlySelected = 0;
    private int previouslySelected = 1;
    public PlayerActions input;

    private bool selectorInputAssigned;

    private bool directionHeld;

    void Update()
    {
        if(input != null)
        {
            if (!selectorInputAssigned)
            {
                for(int i = 0; i < selectors.Length; ++i)
                {
                    selectors[i].input = input;
                }
                selectorInputAssigned = true;
            }
            if(input.move.X >= 0.75 && !directionHeld)
            {
                SelectNext();
                directionHeld = true;
            }
            if (input.move.X <= -0.75 && !directionHeld)
            {
                SelectPrevious();
                directionHeld = true;
            }

            if(input.move.X > -0.75 && input.move.X < 0.75)
            {
                directionHeld = false;
            }
        }
    }

    void SelectNext()
    {
        currentlySelected++;
        if (currentlySelected >= selectors.Length)
        {
            currentlySelected = 0;
        }
        selectors[previouslySelected].Deselect();
        selectors[currentlySelected].Select();
        previouslySelected = currentlySelected;
    }

    void SelectPrevious()
    {
        currentlySelected--;
        if (currentlySelected < 0)
        {
            currentlySelected = selectors.Length - 1;
        }
        selectors[previouslySelected].Deselect();
        selectors[currentlySelected].Select();
        previouslySelected = currentlySelected;
    }
}
