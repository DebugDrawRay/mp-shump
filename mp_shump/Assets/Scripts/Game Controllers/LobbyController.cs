using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

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

    public SelectableContainer playerOneLoadout;
    public SelectableContainer playerTwoLoadout;

    public SelectableContainer[] lobbyUi = new SelectableContainer[2];

    public bool playerOneReady;
    public bool playerTwoReady;

    public string nextScene;
     
    void Awake()
    {
        settings = GameSettings.instance;
        controllerListener = PlayerActions.BindActionsWithController();
        keyboardListener = PlayerActions.BindActionsWithKeyboard();
    }

    void Update()
    {
        runStates();
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
                if(settings.playerOneInput.pause.WasPressed)
                {
                    playerOneReady = true;
                }
                if(settings.playerTwoInput.pause.WasPressed)
                {
                    playerTwoReady = true;
                }

                if(playerOneReady && playerTwoReady)
                {
                    currentState = state.EnterGame;
                }
                break;
            case state.EnterGame:
                SetLoadout();
                SceneManager.LoadScene(nextScene);
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
                playerOneLoadout.input = newActions;
            }
            else if (settings.playerTwoInput == null)
            {
                Debug.LogFormat("Assigned Player 2");
                PlayerActions newActions = PlayerActions.BindActionsWithController();
                newActions.Device = InputManager.ActiveDevice;
                settings.playerTwoInput = newActions;
                playerTwoLoadout.input = newActions;

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
                playerOneLoadout.input = newActions;

            }
            else if (settings.playerTwoInput == null)
            {
                Debug.LogFormat("Assigned Player 2");
                PlayerActions newActions = PlayerActions.BindActionsWithKeyboard();
                newActions.Device = InputManager.ActiveDevice;
                settings.playerTwoInput = newActions;
                playerTwoLoadout.input = newActions;

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

    void SetLoadout()
    {
        settings.playerOne = playerOneLoadout.shipContainer.currentLoadoutObject;
        settings.playerOnePrimary = playerOneLoadout.primaryContainer.currentLoadoutObject;
        settings.playerOneSecondary = playerOneLoadout.secondaryContainer.currentLoadoutObject;

        settings.playerTwo = playerTwoLoadout.shipContainer.currentLoadoutObject;
        settings.playerTwoPrimary = playerTwoLoadout.primaryContainer.currentLoadoutObject;
        settings.playerTwoSecondary = playerTwoLoadout.secondaryContainer.currentLoadoutObject;
    }
}
