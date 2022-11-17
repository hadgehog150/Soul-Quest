using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHight : MonoBehaviour
{
    public GameObject boss, enemy1, enemy2, enemy3, wall;


    private void OnTriggerExit2D(Collider2D collision)
    {
        boss.SetActive(true);
        wall.SetActive(true);
        enemy1.SetActive(true);
        enemy2.SetActive(true);
        enemy3.SetActive(true);
        gameObject.SetActive(false);
    }
}
