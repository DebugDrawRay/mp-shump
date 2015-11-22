using UnityEngine;
using System.Collections;

public class player : MonoBehaviour
{
    public enum state
    {
        inactive,
        active,
        atCenter,
        destroyed,
    }
    public state currentState;

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
    public GameObject[] uiSets;

    //Component Cashe
    private status currentStatus;

    //Statics
    public static int distanceFromCenter;

    void Awake()
    {
        initializeComponents();
        distanceFromCenter = Mathf.RoundToInt(Mathf.Abs(transform.position.x));
    }
    void Start()
    {
        foreach (actionController action in availableActions)
        {
            action.input = new controllerListener(playerNumber);
        }
        setupLocalCamera();
        initializeUi();
        tag = "Player" + playerNumber.ToString();
    }

    void initializeComponents()
    {
        currentStatus = GetComponent<status>();
    }

    void initializeUi()
    {
        playerUi newUi = Instantiate(uiSets[playerNumber - 1]).GetComponent<playerUi>();
        newUi.player = gameObject;
    }

    void setupLocalCamera()
    {
        currentCamera = Instantiate(localCamera, transform.position, Quaternion.identity) as GameObject;

        if (playerNumber == 2)
        {
            currentCamera.GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
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
                enableActions(true);
                if(currentStatus)
                {
                    if(currentStatus.destroyed)
                    {
                        currentState = state.destroyed;
                    }
                }
                break;
            case state.destroyed:
                destroyEvent();
                break;
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
    void destroyEvent()
    {
        enableActions(false);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

}
