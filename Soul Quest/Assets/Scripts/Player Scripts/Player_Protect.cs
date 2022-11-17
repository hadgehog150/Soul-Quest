using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Protect : MonoBehaviour
{
    private bool isProtecting;
    public Animator anim;

    public float speed;
    private GameObject player;
    private Rigidbody2D rigidbody2D;

    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = initialPosition;
        target = player.transform.position;

        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = target - transform.position;

        rigidbody2D.MovePosition(transform.position + dir * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.X)) {
            if (!isProtecting) {
                isProtecting = true;
                anim.SetBool("isProtecting", isProtecting);
            }
        }
        if (Input.GetKeyUp(KeyCode.X)) {
            if (isProtecting) {
                isProtecting = false;
                anim.SetBool("isProtecting", isProtecting);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
