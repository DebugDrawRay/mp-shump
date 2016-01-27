using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameController : MonoBehaviour
{
    //States
    public enum gameState
    {
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

    void Awake()
    {
        initializeInstance();
        setUpPlayers();
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
            players[i] = Instantiate(playerObject).GetComponent<player>();
            if (i == 1)
            {
                players[i].transform.rotation = flip;
            }
            players[i].playerNumber = i + 1;
        }
    }

    void Update()
    {
        runGameStates();

        if(Input.GetButtonDown("Start_1") || Input.GetButtonDown("Start_2"))
        {
            Application.LoadLevel("test");
        }
        if (Input.GetButtonDown("Back_1") || Input.GetButtonDown("Back_2"))
        {
            Application.Quit();
        }
    }

    void runGameStates()
    {
        switch (currentState)
        {
            case gameState.Start:
                startPlayers();
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

        if (checkVictor() != 0)
        {
            displayVictor(checkVictor());
            currentState = gameState.Victory;
        }
    }

    void beginGame()
    {
        playerState(player.state.active);
    }

    void startPlayers()
    {
        Vector3 cameraOrigin = new Vector3(-totalFieldLength / 2, 0, 0);

        for (int i = 0; i <= players.Length - 1; i++)
        {
            if (i == 1)
            {
                cameraOrigin = -cameraOrigin;
            }
            cameraOrigin.z = players[i].currentCamera.transform.position.z;
            players[i].currentCamera.transform.position = cameraOrigin;
        }
    }

    bool checkDestination()
    {
        foreach (player player in players)
        {
            float xpos = Mathf.Abs(player.currentCamera.transform.position.x);
            if (xpos <= screenCenterOffset / 2)
            {
                float side = player.currentCamera.transform.position.x / Mathf.Abs(player.currentCamera.transform.position.x);
                Vector3 position = player.currentCamera.transform.position;
                position.x = (screenCenterOffset / 2) * side;
                player.currentCamera.transform.position = position;
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
