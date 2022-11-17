using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    Player_Movement player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (player.key != null)
            {
                Destroy(player.key.gameObject);
                Destroy(gameObject);
                player.key = null;
                
            }
        }
    }
}
