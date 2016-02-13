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

        Vector2 dumShit = target.transform.right + target.transform.position;
        Vector2 pos = dumShit + Mathf.Sign(target.transform.right.x) * spawnOffset;

        Debug.Log(dumShit);

        GameObject newEnemy = Instantiate(enemiesToSend, pos, rot) as GameObject;
    }
}
