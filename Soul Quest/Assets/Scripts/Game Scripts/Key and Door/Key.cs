using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    SpringJoint2D spring;

    void Start()
    {
        spring = GetComponent<SpringJoint2D>();
        spring.enabled = false;
        GameObject backpack = GameObject.FindWithTag("Backpack");
        spring.connectedBody = backpack.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player_Movement player = FindObjectOfType<Player_Movement>();
            spring.enabled = true;
            player.key = this;
        }
    }
}
