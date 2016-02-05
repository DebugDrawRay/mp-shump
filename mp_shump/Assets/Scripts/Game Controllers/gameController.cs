using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class gameController : MonoBehaviour
{
    //States
    public enum gameState
    {
        CheckPlayers,
        Start,
        InGame,
        AtCenter,
        InVersus,
        Paused,
        Victory,
        Results
    }

    public gameState currentState = gameState.InGame;

    //Sub Controllers
    private gameCanvas canvas;
    private levelFactory factory;

    [Header("Player Container")]
    public GameObject playerObject;

    [Header("Play Field Properties")]
    public float totalFieldLength = 500;
    public float screenCenterOffset = 8.9f;

    public GameObject gate;
    private GameObject currentGate;

    public Camera centerCam;
    
    private Quaternion flip = Quaternion.Euler(0, 180, 0);

    public player[] players
    {
        get;
        private set;
    }

    public static gameController instance
    {
        get;
        private set;
    }

    //Input
    private PlayerActions controllerListener;
    private PlayerActions keyboardListener;
    private InputDevice lastActiveDevice;

    [Header("Debug")]
    public bool enableInputForTesting;

    void Awake()
    {
        initializeInstance();
        setUpPlayers();
        controllerListener = PlayerActions.BindActionsWithController();
        keyboardListener = PlayerActions.BindActionsWithKeyboard();
        currentGate = Instantiate(gate, Vector2.zero, Quaternion.identity) as GameObject;
    }
    void Start()
    {
        canvas = gameCanvas.instance;
        factory = levelFactory.instance;
        factory.buildLevel();
    }

    void initializeInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void setUpPlayers()
    {
        players = new player[2];

        for (int i = 0; i <= players.Length - 1; i++)
        {
            Vector3 startPosition = new Vector3(-totalFieldLength / 2, 0, 0);
            players[i] = Instantiate(playerObject).GetComponent<player>();
            if (i == 1)
            {
                startPosition = -startPosition;
                players[i].transform.rotation = flip;
            }
            players[i].playerNumber = i + 1;
            players[i].transform.position = startPosition;
        }
    }

    void Update()
    {
        runGameStates();
    }

    void runGameStates()
    {
        switch (currentState)
        {
            case gameState.CheckPlayers:
                if (inputSetup())
                {
                    currentState = gameState.Start;
                }
                break;
            case gameState.Start:
                if (!canvas.startCountdown(canvas.countdownClock))
                {
                    beginGame();
                    currentState = gameState.InGame;
                }
                break;
            case gameState.InGame:
                if (checkDestination())
                {
                    playerState(player.state.atCenter);
                    currentState = gameState.AtCenter;
                }
                break;
            case gameState.AtCenter:
                if (!canvas.startCountdown(canvas.vsClock))
                {
                    beginVersus();
                    playerState(player.state.inVersus);
                    currentState = gameState.InVersus;
                }
                break;
            case gameState.InVersus:
                centerCam.enabled = true;
                break;
            case gameState.Paused:
                break;
            case gameState.Victory:
                break;
            case gameState.Results:
                break;
        }

        if (currentState != gameState.CheckPlayers && checkVictor() != 0)
        {
            displayVictor(checkVictor());
            currentState = gameState.Victory;
        }
    }

    bool inputSetup()
    {
        if (controllerListener.primary.WasPressed)
        {
            if (!players[0].inputSetup && InputManager.ActiveDevice != lastActiveDevice)
            {
                Debug.LogFormat("Assigned Player 1");
                PlayerActions newActions = PlayerActions.BindActionsWithController();
                lastActiveDevice = InputManager.ActiveDevice;
                newActions.Device = InputManager.ActiveDevice;
                players[0].setupActions(newActions);
            }
            else if (!players[1].inputSetup && InputManager.ActiveDevice != lastActiveDevice)
            {
                Debug.LogFormat("Assigned Player 2");
                PlayerActions newActions = PlayerActions.BindActionsWithController();
                lastActiveDevice = InputManager.ActiveDevice;
                newActions.Device = InputManager.ActiveDevice;
                players[1].setupActions(newActions);
            }
        }
        else if(keyboardListener.primary.WasPressed)
        {
            if (!players[0].inputSetup)
            {
                Debug.LogFormat("Assigned Player 1");
                PlayerActions newActions = PlayerActions.BindActionsWithKeyboard();
                newActions.Device = InputManager.ActiveDevice;
                players[0].setupActions(newActions);
            }
            else if (!players[1].inputSetup)
            {
                Debug.LogFormat("Assigned Player 2");
                PlayerActions newActions = PlayerActions.BindActionsWithKeyboard();
                newActions.Device = InputManager.ActiveDevice;
                players[1].setupActions(newActions);
            }
        }
        if (!players[0].inputSetup || !players[1].inputSetup)
        {
            return false;
        }
        else
        {
            controllerListener.Destroy();
            keyboardListener.Destroy();
            return true;
        }
    }

    void beginGame()
    {
        playerState(player.state.active);
    }

    bool checkDestination()
    {
        foreach (player player in players)
        {
            float xpos = Mathf.Abs(player.transform.position.x);
            if (xpos <= screenCenterOffset / 2)
            {
                Vector3 position = player.currentCamera.transform.position;
                position.x = (screenCenterOffset / 2) * Mathf.Sign(position.x);
                player.transform.position = position;
                return true;
            }
        }
        return false;
    }

    void playerState(player.state state)
    {
        foreach (player player in players)
        {
            player.currentState = state;
        }
    }

    void beginVersus()
    {
        Destroy(currentGate);
    }

    int checkVictor()
    {
        for (int i = 0; i <= players.Length - 1; i++)
        {
            if (players[i].currentState == player.state.destroyed)
            {
                return i + 1;
            }
        }
        return 0;
    }
    void displayVictor(int playerNumber)
    {
        if (playerNumber == 1)
        {
            gameCanvas.instance.displayVictor(2);
        }
        if (playerNumber == 2)
        {
            gameCanvas.instance.displayVictor(1);
        }
    }
}
