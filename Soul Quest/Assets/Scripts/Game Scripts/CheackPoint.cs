using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheackPoint : MonoBehaviour
{
    CameraFollow camera;
    Camera cameramain;

    void Start()
    {
        camera = FindObjectOfType<CameraFollow>();
        cameramain = FindObjectOfType<Camera>();

        if (PlayerPrefs.GetInt("PositionPlayer") == 1)
        {
            transform.position = new Vector2(PlayerPrefs.GetFloat("xPosition"), PlayerPrefs.GetFloat("yPosition"));
            camera.leftLimit = PlayerPrefs.GetFloat("left");
            camera.rightLimit = PlayerPrefs.GetFloat("right");
            camera.upLimit = PlayerPrefs.GetFloat("up");
            camera.downLimit = PlayerPrefs.GetFloat("down");
            cameramain.backgroundColor =new Color(PlayerPrefs.GetFloat("r"), PlayerPrefs.GetFloat("g"), PlayerPrefs.GetFloat("b"));
        }
        else if (PlayerPrefs.GetInt("PositionPlayer") == 0)
        {
            transform.position = new Vector2(30.69f, -43.15f);
            camera.leftLimit = 31.75f;
            camera.rightLimit = 31.75f;
            camera.upLimit = -44.25f;
            camera.downLimit = -44.25f;
            cameramain.backgroundColor = new Color(248, 248, 136);
        }


        if (PlayerPrefs.GetInt("PositionPlayer") == 2)
        {
            transform.position = new Vector2(PlayerPrefs.GetFloat("xPosition"), PlayerPrefs.GetFloat("yPosition"));
            camera.leftLimit = PlayerPrefs.GetFloat("left");
            camera.rightLimit = PlayerPrefs.GetFloat("right");
            camera.upLimit = PlayerPrefs.GetFloat("up");
            camera.downLimit = PlayerPrefs.GetFloat("down");
            cameramain.backgroundColor = new Color(PlayerPrefs.GetFloat("r"), PlayerPrefs.GetFloat("g"), PlayerPrefs.GetFloat("b"));
        }
        else if (PlayerPrefs.GetInt("PositionPlayer") == 0)
        {
            transform.position = new Vector2(30.69f, -43.15f);
            camera.leftLimit = 31.75f;
            camera.rightLimit = 31.75f;
            camera.upLimit = -44.25f;
            camera.downLimit = -44.25f;
            cameramain.backgroundColor = new Color(248f, 248f, 168f);
        }

        if (PlayerPrefs.GetInt("PositionPlayer") == 3)
        {
            transform.position = new Vector2(PlayerPrefs.GetFloat("xPosition"), PlayerPrefs.GetFloat("yPosition"));
            camera.leftLimit = PlayerPrefs.GetFloat("left");
            camera.rightLimit = PlayerPrefs.GetFloat("right");
            camera.upLimit = PlayerPrefs.GetFloat("up");
            camera.downLimit = PlayerPrefs.GetFloat("down");
            cameramain.backgroundColor = new Color(PlayerPrefs.GetFloat("r"), PlayerPrefs.GetFloat("g"), PlayerPrefs.GetFloat("b"));
        }
        else if (PlayerPrefs.GetInt("PositionPlayer") == 0)
        {
            transform.position = new Vector2(30.69f, -43.15f);
            camera.leftLimit = 31.75f;
            camera.rightLimit = 31.75f;
            camera.upLimit = -44.25f;
            camera.downLimit = -44.25f;
            cameramain.backgroundColor = new Color(248f, 248f, 168f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("cheackpoint1"))
        {
            PlayerPrefs.SetInt("PositionPlayer", 1);
            PlayerPrefs.SetFloat("xPosition", transform.position.x);
            PlayerPrefs.SetFloat("yPosition", transform.position.y);
            PlayerPrefs.SetFloat("left", camera.leftLimit);
            PlayerPrefs.SetFloat("right", camera.rightLimit);
            PlayerPrefs.SetFloat("up", camera.upLimit);
            PlayerPrefs.SetFloat("down", camera.downLimit);
            PlayerPrefs.SetFloat("r", cameramain.backgroundColor.r);
            PlayerPrefs.SetFloat("g", cameramain.backgroundColor.g);
            PlayerPrefs.SetFloat("b", cameramain.backgroundColor.b);
        }

        if (collision.CompareTag("cheackpoint2"))
        {
            PlayerPrefs.SetInt("PositionPlayer", 2);
            PlayerPrefs.SetFloat("xPosition", transform.position.x);
            PlayerPrefs.SetFloat("yPosition", transform.position.y);
            PlayerPrefs.SetFloat("left", camera.leftLimit);
            PlayerPrefs.SetFloat("right", camera.rightLimit);
            PlayerPrefs.SetFloat("up", camera.upLimit);
            PlayerPrefs.SetFloat("down", camera.downLimit);
            PlayerPrefs.SetFloat("r", cameramain.backgroundColor.r);
            PlayerPrefs.SetFloat("g", cameramain.backgroundColor.g);
            PlayerPrefs.SetFloat("b", cameramain.backgroundColor.b);
        }

        if (collision.CompareTag("cheackpoint3"))
        {
            PlayerPrefs.SetInt("PositionPlayer", 3);
            PlayerPrefs.SetFloat("xPosition", transform.position.x);
            PlayerPrefs.SetFloat("yPosition", transform.position.y);
            PlayerPrefs.SetFloat("left", camera.leftLimit);
            PlayerPrefs.SetFloat("right", camera.rightLimit);
            PlayerPrefs.SetFloat("up", camera.upLimit);
            PlayerPrefs.SetFloat("down", camera.downLimit);
            PlayerPrefs.SetFloat("r", cameramain.backgroundColor.r);
            PlayerPrefs.SetFloat("g", cameramain.backgroundColor.g);
            PlayerPrefs.SetFloat("b", cameramain.backgroundColor.b);
        }
    }
}
