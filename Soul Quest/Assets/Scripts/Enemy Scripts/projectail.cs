using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectail : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Shild")
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
