using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class gameController : MonoBehaviour
{
    //States
    public enum gameState
    {
        EnterGame,
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

    [Header("Player Information Ui")]
    public GameObject playerInfoUi;

    public GameObject playerOneUi;
    public Vector2 playerOneUiPos;
    public Vector2 playerOneUiAnchorMin;
    public Vector2 playerOneUiAnchorMax;

    public GameObject playerTwoUi;
    public Vector2 playerTwoUiPos;
    public Vector2 playerTwoUiAnchorMin;
    public Vector2 playerTwoUiAnchorMax;

    void Awake()
    {
        initializeInstance();
        
        currentGate = Instantiate(gate, Vector2.zero, Quaternion.identity) as GameObject;
    }

    void Start()
    {
        canvas = gameCanvas.instance;
        factory = levelFactory.instance;

        InitializeUi();
    }

    void InitializeUi()
    {
        GameObject playerOneUi = Instantiate(playerInfoUi, transform.position, Quaternion.identity) as GameObject;
        GameObject playerTwoUi = Instantiate(playerInfoUi, transform.position, Quaternion.identity) as GameObject;

        playerOneUi.transform.SetParent(gameCanvas.instance.transform);
        playerTwoUi.transform.SetParent(gameCanvas.instance.transform);
        RectTransform rectOne = playerOneUi.GetComponent<RectTransform>();
        RectTransform rectTwo = playerTwoUi.GetComponent<RectTransform>();

        rectOne.rotation = Quaternion.Euler(0, 0, 0);
        rectTwo.rotation = Quaternion.Euler(0, 180, 0);

        rectOne.anchoredPosition = playerOneUiPos;
        rectOne.anchorMin = playerOneUiAnchorMin;
        rectOne.anchorMax = playerOneUiAnchorMax;

        rectTwo.anchoredPosition = playerTwoUiPos;
        rectTwo.anchorMin = playerOneUiAnchorMin;
        rectTwo.anchorMax = playerOneUiAnchorMax;

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


    void setUpPlayers(GameSettings settings)
    {
        players = new player[2];

        Vector3 startPosition = new Vector3(-totalFieldLength / 2, 0, 0);

        players[0] = Instantiate(settings.playerOne).GetComponent<player>();
        players[0].GetComponent<weaponController>().primaryWeapons[0] = settings.playerOnePrimary;
        players[0].GetComponent<weaponController>().secondaryWeapons[0] = settings.playerOneSecondary;
        players[0].transform.position = startPosition;
        players[0].playerNumber = 1;

        players[1] = Instantiate(settings.playerTwo).GetComponent<player>();
        players[1].GetComponent<weaponController>().primaryWeapons[0] = settings.playerTwoPrimary;
        players[1].GetComponent<weaponController>().secondaryWeapons[0] = settings.playerTwoSecondary;
        players[1].transform.position = -startPosition;
        players[1].transform.rotation = flip;
        players[1].playerNumber = 2;

        players[0].enemy = players[1];
        players[1].enemy = players[0];

        players[0].setupActions(settings.playerOneInput);
        players[1].setupActions(settings.playerTwoInput);
    }

    void Update()
    {
        runGameStates();
    }

    void runGameStates()
    {
        switch (currentState)
        {
            case gameState.EnterGame:
                if(LoadSettings())
                {
                    currentState = gameState.CheckPlayers;
                }
                break;
            case gameState.CheckPlayers:
                if (players[0].playerReady && players[1].playerReady)
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

    bool LoadSettings()
    {
        setUpPlayers(GameSettings.instance);
        factory.buildLevel(GameSettings.instance.selectedLevel);
        return true;
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
