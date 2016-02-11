using UnityEngine;
using System.Collections;

public class SendEnemiesItem : MonoBehaviour, IUseableItem
{
    public GameObject enemiesToSend;
    public Vector2 spawnOffset;
    public void useItem(player target)
    {
        Quaternion rot = target.transform.rotation;
        rot = Quaternion.Euler(rot.eulerAngles.x, 180 - rot.eulerAngles.y, rot.eulerAngles.z);

        Vector2 dumShit = target.transform.forward + target.transform.position;
        Vector2 pos = dumShit + spawnOffset;

        GameObject newEnemy = Instantiate(enemiesToSend, pos, rot) as GameObject;
    }
}
