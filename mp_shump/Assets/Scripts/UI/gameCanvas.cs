using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class gameCanvas : MonoBehaviour
{
    public GameObject countdownClock;
    public GameObject vsClock;
    private countdown currentClock;
    public Text victor;
    public Text distanceMeter;
    public float distanceOffset = 4;
    public Image centerMark;
    public static gameCanvas instance
    {
        get;
        private set;
    }

    void Awake()
    {
        initializeInstance();
    }

    void Start()
    {
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

    void Update()
    {
        trackDistance();
    }

    void trackDistance()
    {
        if (centerMark && distanceMeter)
        {
            float dist = player.distanceFromCenter - distanceOffset;
            Debug.Log(dist);
            distanceMeter.text = dist.ToString();
            if (dist <= 0)
            {
                Destroy(centerMark.gameObject);
                Destroy(distanceMeter.gameObject);
                centerMark = null;
                distanceMeter = null;
            }
        }
    }

    public bool startCountdown(GameObject clock)
    {
        if (currentClock == null)
        {
            currentClock = Instantiate(clock).GetComponent<countdown>();
            currentClock.gameObject.transform.SetParent(transform);
            currentClock.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        if(currentClock.isActive == false)
        {
            Destroy(currentClock.gameObject);
            currentClock = null;
            return false;
        }
        return currentClock.isActive;
    }

    public void displayVictor(int playerNumber)
    {
        victor.text = "Player " + playerNumber.ToString() + " Wins!";
    }
}
