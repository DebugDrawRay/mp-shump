using UnityEngine;
using System.Collections;
using DG.Tweening;

public class playerCamController : MonoBehaviour
{
    public enum state
    {
        inactive,
        active,
        atDestination,
        destroyed
    }
    public state currentState = state.inactive;

    //Local state control
    public bool isActive = false;
    public bool isDestroyed = false;

    [Header("Movement properties")]
    public float scrollSpeed;
    public float defaultCamFloat = -10;

    public GameObject playerShip;
    private GameObject ship;
    private float facingDirection;

    public bool destinationReached
    {
        get;
        private set;
    }

    private Rigidbody2D rigid;

    //Game settings
    private int playerNumber;
    private float destinationXPos;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        transform.position += new Vector3(0, 0, defaultCamFloat);
    }

    public void setUpPlayer(int _playerNumber, float _destinationXPos, GameObject enemyObject)
    {
        playerNumber = _playerNumber;
        destinationXPos = _destinationXPos;

        if(playerNumber == 1)
        {
            facingDirection = 1;
        }
        if (playerNumber == 2)
        {
            facingDirection = -1;
            GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
        }
        createNewShip(enemyObject, facingDirection);

    }

    void createNewShip(GameObject enemyObject, float facing)
    {
        Vector2 origin = transform.position;
        ship = Instantiate(playerShip, transform.position, Quaternion.identity) as GameObject;
        ship.transform.SetParent(transform);
        ship.transform.position = origin;

        ship.GetComponent<playerShip>().constructShip(playerNumber, enemyObject, facing);

    }

    void enable()
    {
        isActive = true;
        ship.GetComponent<playerShip>().isActive = isActive;
    }

    void Update()
    {
        runStates();
    }

    void runStates()
    {
        switch(currentState)
        {
            case state.inactive:
                if(isActive)
                {
                    enable();
                    currentState = state.active;
                }
                break;
            case state.active:
                if(checkDestination())
                {
                    currentState = state.atDestination;
                }
                break;
            case state.atDestination:
                if(ship.GetComponent<status>().lives <= 0)
                {
                    currentState = state.destroyed;
                }
                break;
            case state.destroyed:
                isDestroyed = true;
                break;
        }
    }
    //combine with below
    bool checkDestination()
    {
        if (!checkAtDestination())
        {
            Vector2 scrollVelocity = (Vector2.right * facingDirection) * scrollSpeed;
            transform.Translate(scrollVelocity * Time.deltaTime);
            return false;
        }
        else
        {
            if(!destinationReached)
            {
                destinationReached = true;
                transform.position = new Vector3(destinationXPos * -facingDirection, 0, defaultCamFloat);
                return true;
            }
            return false;
        }
    }

    bool checkAtDestination()
    {
        if(Mathf.Abs(transform.position.x) <= Mathf.Abs(destinationXPos))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
