using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour
{
    public GameObject playerOne;
    public GameObject playerTwo;

    public PlayerActions playerOneInput;
    public PlayerActions playerTwoInput;

    public weapon[] playerOnePrimaries = new weapon[3];
    public weapon[] playerTwoPrimaries = new weapon[3];

    public weapon playerOneSecondary;
    public weapon playerTwoSecondary;

    public levelAsset selectedLevel;

    public static GameSettings instance
    {
        get;
        private set;
    }

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
