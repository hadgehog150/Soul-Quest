using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keese : MonoBehaviour
{
    public float speed;

    public float maxRandomPositionX, minRandomPositionX;
    public float maxRandomPositionY, minRandomPositionY;

    public float visionRadious;

    public int hp;

    public string destroyState;
    public float timeForDisable;

    private GameObject player;
    private Rigidbody2D rigidbody2D;
    private bool angry;
    private Vector3 initialPosition;

    private Animator animator;

    private bool attacing;
    public float attackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody2D = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
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
            }
        }

        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = target - transform.position;

        if (angry)
        {
            angry = false;
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, initialPosition) == 0)
            {
                initialPosition = new Vector2(Random.RandomRange(maxRandomPositionX, minRandomPositionX), Random.RandomRange(maxRandomPositionY, minRandomPositionY));
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(destroyState) && stateInfo.normalizedTime >= 1)
        {
            Destroy(gameObject);
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
            animator.Play(destroyState);
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
