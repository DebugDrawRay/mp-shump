using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectableContainer : MonoBehaviour
{
    private enum state
    {
        CheckInput,
        SelectLoadout,
        Ready
    }
    private state currentState;

    public GameObject checkInputLogo;

    public LoadoutContainer shipContainer;
    public LoadoutContainer primaryContainer;
    public LoadoutContainer secondaryContainer;

    public GameObject readyLogo;

    private LoadoutContainer[] selectors = new LoadoutContainer[3];

    private int currentlySelected = 0;
    private int previouslySelected = 1;
    public PlayerActions input;

    private bool selectorInputAssigned;

    private bool directionHeld;

    void Awake()
    {
        selectors[0] = shipContainer;
        selectors[1] = primaryContainer;
        selectors[2] = secondaryContainer;
    }

    void Update()
    {
        inputListener();
        runStates();
    }

    void runStates()
    {
        switch(currentState)
        {
            case state.CheckInput:
                if (input != null)
                {
                    checkInputLogo.SetActive(false);
                    for (int i = 0; i < selectors.Length; ++i)
                    {
                        selectors[i].gameObject.SetActive(true);
                    }
                    readyLogo.SetActive(false);

                    currentState = state.SelectLoadout;
                }
                break;
            case state.SelectLoadout:
                if (input.pause.WasPressed)
                {
                    checkInputLogo.SetActive(false);
                    for (int i = 0; i < selectors.Length; ++i)
                    {
                        selectors[i].gameObject.SetActive(false);
                    }
                    readyLogo.SetActive(true);
                    currentState = state.Ready;
                }
                break;
            case state.Ready:
                break;
        }
    }
    void inputListener()
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
