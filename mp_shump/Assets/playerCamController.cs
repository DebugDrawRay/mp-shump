using UnityEngine;
using System.Collections;

public class playerCamController : MonoBehaviour
{
    public int playerNumber;
    public float scrollSpeed;
    public GameObject playerShip;

    private GameObject ship;
    private Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ship = Instantiate(playerShip, transform.position, Quaternion.identity) as GameObject;
        ship.transform.SetParent(transform);
        Vector2 pos = transform.position;
        ship.transform.position = pos;
        ship.GetComponent<playerShip>().playerNumber = playerNumber;

        if(playerNumber == 2)
        {
            GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
        }

    }

    void Update()
    {
        rigid.velocity = Vector2.right * scrollSpeed;
    }
}
