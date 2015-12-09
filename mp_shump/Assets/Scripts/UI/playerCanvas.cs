using UnityEngine;
using System;

public class playerCanvas : MonoBehaviour
{
    public GameObject[] ui = new GameObject[2];
    public GameObject screenFlash;
    private GameObject currentUi;
    private player player;
    private Canvas thisCanvas;

    public void init(player parent)
    {
        player = parent;
        thisCanvas = GetComponent<Canvas>();
        thisCanvas.worldCamera = player.currentCamera.GetComponent<Camera>();
        currentUi = ui[player.playerNumber - 1];
        currentUi = Instantiate(currentUi);
        currentUi.transform.SetParent(transform);
        currentUi.GetComponent<playerUi>().player = player.gameObject;

        player.GetComponent<status>().StatusChanged += new StatusChangedEvent(flashScreen);
    }

    private void flashScreen()
    {
        GameObject flash = Instantiate(screenFlash) as GameObject;
        flash.transform.SetParent(transform);
    }

    public void Update()
    {

    }
}