using UnityEngine;
using System.Collections;
using DG.Tweening;
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
    public GameObject playerCanvas;
    public GameObject[] availableUI = new GameObject[2];

    [Header("Respawning")]
    public float invulPeriod;
    private float currentInvul;

    private bool positionReset = false;
    //Component Cashe
    private status currentStatus;

    //Statics
    public static float distanceFromCenter;

    void Awake()
    {
        initializeComponents();
        distanceFromCenter = 500;

        currentInvul = invulPeriod;
    }
    void Start()
    {
        initializeUi();
        availableActions = GetComponents<actionController>();
        foreach (actionController action in availableActions)
        {
            action.input = new controllerListener(playerNumber);
        }
        setupLocalCamera();
        tag = "Player" + playerNumber.ToString();
    }

    void initializeComponents()
    {
        currentStatus = GetComponent<status>();
    }

    void initializeUi()
    {
        playerUiController newUI = Instantiate(availableUI[playerNumber - 1]).GetComponent<playerUiController>();
        IUiBroadcast[] availableBroadcasters = GetComponents<IUiBroadcast>();
        foreach(IUiBroadcast broadcaster in availableBroadcasters)
        {
            broadcaster.targetUI = newUI;

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

    void Update()
    {
        runStates();
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
                enableActions(false);
                toggleCollisions(false);
                respawnEvent();

                currentInvul -= Time.deltaTime;
                if(currentInvul <= 0)
                {
                    GetComponent<SpriteRenderer>().enabled = true;
                    toggleCollisions(true);
                    positionReset = false;
                    currentState = previousState;
                }
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
            currentInvul = invulPeriod;
            positionReset = true;
        }

        GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
        
        recenterPlayer();
        
    }
    void destroyEvent()
    {
        enableActions(false);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject);

    }

}
