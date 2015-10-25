using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class gameCanvas : MonoBehaviour
{
    public countdown countdown;
    public countdown vsCountdown;
    public Text victor;
    private List<IcanvasObject> canvasObjects;

    public static gameCanvas instance
    {
        get;
        private set;
    }

    void Awake()
    {
        initializeInstance();
        canvasObjects = new List<IcanvasObject>();
    }

    void Start()
    {
        runCountdown();
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

    void Update()
    {
        checkObjects();
    }

    void checkObjects()
    {
        if (canvasObjects.Count > 0)
        {
            foreach (IcanvasObject obj in canvasObjects)
            {
                if (!obj.isActive)
                {
                    canvasObjects.Remove(obj);
                }
            }
        }
    }

    public void runCountdown()
    {
        countdown.isActive = true;
    }
    public void runVersusCountdown()
    {
        vsCountdown.isActive = true;
    }
    public void displayVictor(int playerNumber)
    {
        victor.text = "Player " + playerNumber.ToString() + " Wins!";
    }
}
