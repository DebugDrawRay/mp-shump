using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class gameCanvas : MonoBehaviour
{
    public GameObject clock;
    private countdown currentClock;
    public Text victor;
    public Text distanceMeter;
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
        distanceMeter.text = (player.distanceFromCenter - gameController.instance.screenCenterOffset/2).ToString();
        Debug.Log(player.distanceFromCenter);
        if(player.distanceFromCenter <= 0 && centerMark)
        {
            Destroy(centerMark.gameObject);
            centerMark = null;
        }
    }

    public bool startCountdown()
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
