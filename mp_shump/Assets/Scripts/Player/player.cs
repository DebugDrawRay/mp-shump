﻿using UnityEngine;
using System.Collections;
using InControl;

public class player : MonoBehaviour
{
    public enum state
    {
        inactive,
        active,
        atCenter,
        inVersus,
        respawning,
        destroyed
    }
    public state currentState;
    public state previousState;

    [Header("Player Properties")]
    public int playerNumber;
    public actionController[] availableActions;

    [Header("Scrolling Properties")]
    public bool isScrolling;
    public float scrollSpeed;

    [Header("Camera Properties")]
    public GameObject localCamera;
    public GameObject currentCamera
    {
        get;
        private set;
    }

    public float defaultCamFloat;

    [Header("UI Properties")]
    public LocalUiController localUi;
    public GameObject[] availableUI = new GameObject[2];

    [Header("Respawning")]
    public float invulPeriod;
    public float flyInLength;
    private float currentInvul;
    private float currentFlyIn;

    private bool positionReset = false;
    //Component Cashe
    private status currentStatus;

    //Statics
    public static float distanceFromCenter;

    //Input
    public bool inputSetup = false;

    private PlayerActions keyboardListener;

    void Awake()
    {
        initializeComponents();
        distanceFromCenter = 500;

        currentInvul = invulPeriod;
        keyboardListener = PlayerActions.BindActionsWithKeyboard();
    }
    void Start()
    {
        initializeUi();
        availableActions = GetComponents<actionController>();

        setupLocalCamera();
        tag = "Player" + playerNumber.ToString();
    }

    void initializeComponents()
    {
        currentStatus = GetComponent<status>();
    }

    void initializeUi()
    {
        //playerUiController newUI = Instantiate(availableUI[playerNumber - 1]).GetComponent<playerUiController>();
        
        IStatBroadcast[] availableBroadcasters = GetComponents<IStatBroadcast>();
        foreach(IStatBroadcast broadcaster in availableBroadcasters)
        {
            broadcaster.targetLocalUi = localUi;
        }
    }

    void setupLocalCamera()
    {
        currentCamera = Instantiate(localCamera, transform.position, Quaternion.identity) as GameObject;

        if (playerNumber == 2)
        {
            currentCamera.GetComponent<Camera>().rect = new Rect(0f, 0f, 1f, 0.5f);
        }
        currentCamera.transform.position += new Vector3(0, 0, defaultCamFloat);
        transform.SetParent(currentCamera.transform);
    }

    public void setupActions(PlayerActions input)
    {
        foreach (actionController action in availableActions)
        {
            action.input = input;
        }
        inputSetup = true;
        keyboardListener.Destroy();
    }

    void Update()
    {
        if (!inputSetup)
        {
            if (keyboardListener.primary.WasPressed)
            {
                PlayerActions newActions = PlayerActions.BindActionsWithKeyboard();
                newActions.Device = InputManager.ActiveDevice;
                setupActions(newActions);
            }
        }
        else
        {
            runStates();
        }
        distanceFromCenter = Mathf.RoundToInt(Mathf.Abs(transform.position.x) - 4);
    }

    /// <summary>
    /// State Control
    /// </summary>
    void runStates()
    {
        switch (currentState)
        {
            case state.inactive:
                enableActions(false);
                break;
            case state.active:    
                enableActions(true);
                runScroll();
                break;
            case state.atCenter:
                enableActions(false);
                recenterPlayer();
                break;
            case state.inVersus:
                enableActions(true);
                break;
            case state.respawning:
                if(previousState == state.active)
                {
                    runScroll();
                }
                respawnEvent();
                break;
            case state.destroyed:
                destroyEvent();
                break;
        }
        runUniversalState();
    }

    void runUniversalState()
    {
        if (currentStatus)
        {
            if(currentStatus.respawning)
            {
                enableActions(false);
                toggleCollisions(false);
                previousState = currentState;
                currentState = state.respawning;
            }
            if (currentStatus.destroyed)
            {
                currentState = state.destroyed;
            }
        }
    }
    void enableActions(bool enable)
    {
        foreach(actionController actions in availableActions)
        {
            actions.isActive = enable;
        }
    }
    void runScroll()
    {
        Vector2 scrollVelocity = transform.right * scrollSpeed;
        currentCamera.transform.Translate(scrollVelocity * Time.deltaTime);
    }
    void recenterPlayer()
    {
        Vector3 camPos = localCamera.GetComponent<Camera>().ScreenToWorldPoint(Vector2.zero);
        Vector3 newPos = transform.localPosition;
        newPos.z = 10;
        transform.localPosition = Vector3.Lerp(newPos, camPos, .05f);
    }
    void toggleCollisions(bool enabled)
    {
        foreach (Collider2D col in GetComponents<Collider2D>())
        {
            col.enabled = enabled;
        }
    }
    void respawnEvent()
    {
        if(!positionReset)
        {
            transform.localPosition = -transform.right * 5;
            currentFlyIn = flyInLength;
            currentInvul = invulPeriod;
            positionReset = true;
        }

        GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;

        currentInvul -= Time.deltaTime;
        currentFlyIn -= Time.deltaTime;

        if(currentFlyIn > 0)
        {
            recenterPlayer();
        }
        if (currentFlyIn <= 0)
        {
            enableActions(true);
        }
        if (currentInvul <= 0)
        {
            toggleCollisions(true);
            GetComponent<SpriteRenderer>().enabled = true;
            positionReset = false;
            currentState = previousState;
        }
        
    }
    void destroyEvent()
    {
        enableActions(false);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject);

    }

}
