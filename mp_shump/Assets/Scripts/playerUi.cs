using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerUi : MonoBehaviour
{
    private Vector2 uiOffset = new Vector2(98, 98);

    [Header("Ui Elements")]
    public Image armorMeter;
    public Image shieldMeter;

    public Image currentWeapon;
    public Image currentMissile;

    public Text livesCounter;
    public Text bombsCounter;

    public Sprite[] laserSprites;
    public Sprite[] missileSprites;

    //player properties
    private status playerStatus;
    private shieldController playerShield;
    private laserController playerLaser;
    private missileController playerMissile;

    public void setUpUi(int number, status status, laserController laser, missileController missile, shieldController shield)
    {
        playerStatus = status;
        playerLaser = laser;
        playerMissile = missile;
        playerShield = shield;

        if (number == 1)
        {
            GetComponent<RectTransform>().anchoredPosition = uiOffset;
        }
        if (number == 2)
        {
            GetComponent<RectTransform>().anchoredPosition = -uiOffset;
        }
    }
    void Update()
    {
        float currentHealth = playerStatus.health / playerStatus.baseHealth;
        armorMeter.fillAmount = currentHealth;
        float currentShields = playerShield.power / playerShield.maxPower;
        shieldMeter.fillAmount = currentShields;

        currentWeapon.sprite = laserSprites[playerLaser.weaponLevel];
        currentMissile.sprite = missileSprites[playerLaser.missileLevel];

        livesCounter.text = playerStatus.lives.ToString();
        bombsCounter.text = playerLaser.bombs.ToString();

        if(playerStatus.hit)
        {
            GetComponent<reactionShake>().shake();
        }
    }

}
