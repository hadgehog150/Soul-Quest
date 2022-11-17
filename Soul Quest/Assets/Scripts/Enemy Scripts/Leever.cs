using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leever : MonoBehaviour
{
    private bool isAttaking;
    private Animator anim;
    private Vector3 initialPosition;
    private bool angry;
    private bool attacking;//атакует не атакует
    private Rigidbody2D rigidbody2D;

    public int hp;

    public string destroyState;
    public float timeForDisable;

    public float visionRadious;
    public float speed;

    private GameObject player;

    private bool attacing;
    public float attackSpeed;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody2D = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    void Update()
    {
        Vector3 target = initialPosition;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, visionRadious, 1 << LayerMask.NameToLayer("Default"));

        Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                target = player.transform.position;
                angry = true;
                anim.SetBool("spawning",angry);
            }
        }

        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = target - transform.position;


        if (angry)
        {
            angry = false;
           
            if (!attacking)
            {
                
                attacking = true;
                anim.SetBool("attacking", attacking);
            }
            rigidbody2D.MovePosition(transform.position + dir * speed/0.5f * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
            if (attacking)
            {
                attacking = false;
                anim.SetBool("spawning", angry);
                anim.SetBool("attacking", attacking);
            }
        }

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(destroyState) && stateInfo.normalizedTime >= 1)
        {
           
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadious);
    }

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Attack")
        {
            anim.Play(destroyState);
            speed = 0;
            yield return new WaitForSeconds(timeForDisable);

            foreach (Collider2D c in GetComponents<Collider2D>())
            {
                c.enabled = false;
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

    public void Attacked()
    {
        --hp;
    }
}
