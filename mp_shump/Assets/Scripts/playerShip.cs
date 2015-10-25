using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerShip : MonoBehaviour
{

    public bool isActive
    {
        get;
        set;
    }


    public GameObject[] playerShips;
    private GameObject thisShip;

    public GameObject[] ui;

    public void constructShip(int playerNumber, GameObject enemyObject, float facing)
    {
        thisShip = Instantiate(playerShips[playerNumber - 1], transform.position, Quaternion.identity) as GameObject;
        thisShip.transform.SetParent(transform);
        createUi(playerNumber);
        initializeComponents(new controllerListener(playerNumber), this.gameObject, enemyObject, facing);
    }

    void initializeComponents(IinputListener input, GameObject parentObject, GameObject enemyObject, float facing)
    {
        List<actionController> actions = new List<actionController>();
        actions.Add(GetComponent<laserController>());
        actions.Add(GetComponent<missileController>());
        actions.Add(GetComponent<shieldController>());
        actions.Add(GetComponent<engine>());

        for(int i = 0; i <= actions.Count - 1; i++)
        {
            actions[i].construct(input, parentObject, enemyObject, facing);
        }
    }

    void createUi(int number)
    {
        GameObject newUi = Instantiate(ui[number - 1]);
        playerUi _ui = newUi.GetComponent<playerUi>();
        newUi.transform.SetParent(gameCanvas.instance.transform);
        _ui.setUpUi(number, GetComponent<status>(), GetComponent<laserController>(), GetComponent<missileController>(), GetComponent<shieldController>());

    }

    void Update()
    {
        GetComponent<engine>().enabled = isActive;
        GetComponent<laserController>().enabled = isActive;

        if(GetComponent<status>().hit)
        {
            thisShip.GetComponent<reactionShake>().shake();
        }
    }

}
