using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    public Transform posToGo;
    public Vector3 playerChange;
    public bool check;
    public Color Color;

    GameObject playerGo;
    CameraFollow camera;
    Camera cameramain;

    public float left, right, up, down;

    void Start()
    {
        camera = FindObjectOfType<CameraFollow>();
        cameramain = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (check && Input.GetKeyDown(KeyCode.F))
        {
            check = false;
            cameramain.backgroundColor = Color;
            playerGo.transform.position = playerChange;
            camera.leftLimit = left;
            camera.rightLimit = right;
            camera.upLimit = up;
            camera.downLimit = down;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            check = true;
            playerGo = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            check = false;
            playerGo = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            check = true;
            playerGo = collision.gameObject;
        }
    }
}
