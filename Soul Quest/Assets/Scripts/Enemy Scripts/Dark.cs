using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dark : MonoBehaviour
{
    public float speed;

    private float x, y;
    private bool isWalking;
    private Rigidbody2D rigidbody;
    private Animator anim;

    public int hp;

    public string destroyState;

    public float timeForDisable;

    SpriteRenderer spriteRenderer;

    private bool attacing;
    public float attackSpeed;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

       

        if (hp<=0)
        { 
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName(destroyState) && stateInfo.normalizedTime >= 1)
            {
                Destroy(gameObject);
            }
        }

        

        rigidbody.velocity = new Vector2(x * speed, y * speed);

        if (x != 0 || y != 0)
        {
            Move();

            if (!isWalking)
            {
                isWalking = true;
                x = 0;
                y = 0;
                anim.SetBool("isWalking", isWalking);
            }
        }
        else
        {
            if (isWalking)
            {
                isWalking = false;
                anim.SetBool("isWalking", isWalking);
            }
        }
    }

    public void Move()
    {
        anim.SetFloat("X", x);
        anim.SetFloat("Y", y);
    }

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Attack")
        {
            hp--;
            if (hp<= 0)
            {
                anim.Play(destroyState);
                spriteRenderer.sortingOrder = 1;
                yield return new WaitForSeconds(timeForDisable);

                foreach (Collider2D c in GetComponents<Collider2D>())
                {
                    c.enabled = false;
                }
            }
            
        }

        if (collision.tag == "Player")
        {
            if (!attacing)
            {
                StartCoroutine(Attack(attackSpeed));
                collision.SendMessage("Attacked");
            }

        }
    }

    IEnumerator Attack(float seconds)
    {
        attacing = true;
        yield return new WaitForSeconds(seconds);
        attacing = false;
    }
}
