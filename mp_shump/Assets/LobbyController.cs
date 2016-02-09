using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    private enum state
    {
        AssignControllers,
        SelectLoadout,
        EnterGame
    }
    private state currentState;

    private PlayerActions controllerListener;
    private PlayerActions keyboardListener;

    private GameSettings settings;

    private PlayerStateControl playerOne;
    private PlayerStateControl playerTwo;

    public SelectableContainer[] lobbyUi = new SelectableContainer[2];

    public class PlayerStateControl
    {
        public enum state
        {
            SelectLoadout,
            EnterGame
        }
        public state currentState;

        public SelectableContainer targetUI;

        public PlayerStateControl(SelectableContainer targetLobbyUI, PlayerActions playerInput)
        {
            targetUI = targetLobbyUI;
            targetUI.input = playerInput;
        }

        public void runStates()
        {
            switch (currentState)
            {
                case state.SelectLoadout:
                    break;
                case state.EnterGame:
                    break;
            }
        }
    }
    

    void Awake()
    {
        settings = GameSettings.instance;
        controllerListener = PlayerActions.BindActionsWithController();
        keyboardListener = PlayerActions.BindActionsWithKeyboard();
    }

    void Update()
    {
        runStates();
        if(playerOne != null)
        {
            playerOne.runStates();
        }
        if(playerTwo != null)
        {
            playerTwo.runStates();
        }
    }

    void runStates()
    {
        switch(currentState)
        {
            case state.AssignControllers:
                if(inputSetup())
                {
                    currentState = state.SelectLoadout;
                }
                break;
            case state.SelectLoadout:
                break;
            case state.EnterGame:
                break;
        }
    }

    bool inputSetup()
    {
        if (controllerListener.primary.WasPressed)
        {
            if (settings.playerOneInput == null)
            {
                Debug.LogFormat("Assigned Player 1");
                PlayerActions newActions = PlayerActions.BindActionsWithController();
                newActions.Device = InputManager.ActiveDevice;
                settings.playerOneInput = newActions;
                playerOne = new PlayerStateControl(lobbyUi[0], newActions);

            }
            else if (settings.playerTwoInput == null)
            {
                Debug.LogFormat("Assigned Player 2");
                PlayerActions newActions = PlayerActions.BindActionsWithController();
                newActions.Device = InputManager.ActiveDevice;
                settings.playerTwoInput = newActions;
                playerOne = new PlayerStateControl(lobbyUi[1], newActions);

            }
        }
        else if(keyboardListener.primary.WasPressed)
        {
            if (settings.playerOneInput == null)
            {
                Debug.LogFormat("Assigned Player 1");
                PlayerActions newActions = PlayerActions.BindActionsWithKeyboard();
                newActions.Device = InputManager.ActiveDevice;
                settings.playerOneInput = newActions;
                playerOne = new PlayerStateControl(lobbyUi[0], newActions);

            }
            else if (settings.playerTwoInput == null)
            {
                Debug.LogFormat("Assigned Player 2");
                PlayerActions newActions = PlayerActions.BindActionsWithKeyboard();
                newActions.Device = InputManager.ActiveDevice;
                settings.playerTwoInput = newActions;
                playerOne = new PlayerStateControl(lobbyUi[1], newActions);

            }
        }
        if (settings.playerOneInput != null && settings.playerTwoInput != null)
        {
            controllerListener.Destroy();
            keyboardListener.Destroy();

            return true;
        }
        else
        {
            return false;
        }
    }
}
