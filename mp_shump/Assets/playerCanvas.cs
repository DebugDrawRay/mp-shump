using UnityEngine;
using System.Collections;

public class playerCanvas : MonoBehaviour
{
    public GameObject[] ui = new GameObject[2];

    private GameObject currentUi;
    public player player;
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
    }
}
