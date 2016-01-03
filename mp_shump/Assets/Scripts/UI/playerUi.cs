using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerUi : MonoBehaviour
{
    private Vector2 uiOffset = new Vector2(60, 60);

    [Header("Ui Elements")]
    public Image armorMeter;
    public Image shieldMeter;

    public Image currentLaser;
    public Image currentMissile;

    public Image laserMeter;
    public Image missileMeter;

    public Text livesCounter;
    public Text bombsCounter;

    public Sprite[] laserSprites;
    public Sprite[] missileSprites;

    //player properties
    public GameObject player;

    private status playerStatus;
    private shieldController playerShield;
    private weapon playerPrimary;
    private weapon playerSecondary;

    void Start()
    {
        int number = player.GetComponent<player>().playerNumber;
        if (number == 1)
        {
            GetComponent<RectTransform>().anchoredPosition = uiOffset;
        }
        if (number == 2)
        {
            GetComponent<RectTransform>().anchoredPosition = -uiOffset;
        }

        playerStatus = player.GetComponent<status>();
        playerShield = player.GetComponent<shieldController>();
        playerPrimary = player.GetComponent<weaponController>().currentPrimary;
        playerSecondary = player.GetComponent<weaponController>().currentSecondary;

    }
    void Update()
    {
        float currentHealth = playerStatus.currentHealth / playerStatus.baseHealth;
        armorMeter.fillAmount = currentHealth;

        float currentShields = playerShield.currentLifetime / playerShield.maxLifetime;
        shieldMeter.fillAmount = currentShields;

        currentLaser.sprite = laserSprites[0];

        currentMissile.sprite = missileSprites[0];

        int currentLives = playerStatus.lives;
        livesCounter.text = currentLives.ToString();

    }

}
