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

    //Direct references
    private GameObject canvas;

    
    public GameObject playerObject;

    [Header("Play Field Properties")]
    public float totalFieldLength = 500;
    public float screenCenterOffset = 8.9f;

    public GameObject gate;
    private GameObject currentGate;

    public GameObject[] players
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
    }

    void initializeInstance()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    void setUpPlayers()
    {
        players = new GameObject[2];
        Vector2 spawnOrigin = new Vector2(-totalFieldLength / 2, 0);
        float destination = -screenCenterOffset / 2;

        for (int i = 0; i <= players.Length - 1; i++)
        {
            if (i == 1)
            {
                spawnOrigin = -spawnOrigin;
                destination = -destination;
            }
            players[i] = Instantiate(playerObject, spawnOrigin, Quaternion.identity) as GameObject;
            playerCamController prop = players[i].GetComponent<playerCamController>();
        }

        players[0].GetComponent<playerCamController>().setUpPlayer(1, destination, players[1]);
        players[1].GetComponent<playerCamController>().setUpPlayer(2, destination, players[0]);

        changePlayerStatus(false);
    }

    void Update()
    {
        runGameStates();
    }

    void runGameStates()
    {
        switch (currentState)
        {
            case gameState.Start:
                if (countdownEnd())
                {
                    beginGame();
                    currentState = gameState.InGame;
                }
                break;
            case gameState.InGame:
                if(checkDestinations())
                {
                    runVersusCountdown();
                    currentState = gameState.AtCenter;
                }
                break;
            case gameState.AtCenter:
                if(versusCountdownEnd())
                {
                    beginVersus();
                    currentState = gameState.InVersus;
                }
                break;
            case gameState.InVersus:
                if(checkVictor() != 0)
                {
                    displayVictor(checkVictor());
                    currentState = gameState.Victory;
                }
                break;
            case gameState.Paused:
                break;
            case gameState.Victory:
                break;
            case gameState.Results:
                break;
        }
    }

    bool countdownEnd()
    {
        return !gameCanvas.instance.countdown.isActive;
    }

    void beginGame()
    {
        changePlayerStatus(true);
        currentGate = Instantiate(gate, Vector2.zero, Quaternion.identity) as GameObject;
    }

    bool checkDestinations()
    {
        foreach (GameObject player in players)
        {
            return player.GetComponent<playerCamController>().destinationReached;
        }
        return false;
    }

    void runVersusCountdown()
    {
        gameCanvas.instance.runVersusCountdown();
    }

    void changePlayerStatus(bool active)
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<playerCamController>().isActive = active;
        }
    }

    bool versusCountdownEnd()
    {
        return !gameCanvas.instance.vsCountdown.isActive;
    }

    void beginVersus()
    {
        Destroy(currentGate);
    }

    int checkVictor()
    {
        for (int i = 0; i <= players.Length - 1; i++)
        {
            if(players[i].GetComponent<playerCamController>().isDestroyed)
            {
                players[i].GetComponent<playerCamController>().isActive = false;
                if (players[i])
                {
                    Destroy(players[i]);
                }

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
