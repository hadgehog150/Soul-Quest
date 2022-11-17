using UnityEngine;
using System.Collections;

public class Octorok : MonoBehaviour
{
    public Vector2 A, B;
    public float speed;
    public float attackSpeed = 2f;
    public GameObject projectile;

    public string destroyState;
    public float timeForDisable;

    public int hp;


    private Animator animator;

    public float visionRadious;

    private GameObject player;
    private Rigidbody2D rigidbody2D;
    private bool angry;
    private Vector3 initialPosition, target, dir;
    private bool attacing;

    public DropItem[] dropList;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody2D = GetComponent<Rigidbody2D>();
        initialPosition = A;
        animator = GetComponent<Animator>();
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
            }
        }

        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position).normalized;

        if (angry)
        {
            angry = false;
            animator.SetFloat("X", dir.x);
            animator.SetFloat("Y", dir.y);
            if (!attacing) StartCoroutine(Attack(attackSpeed));
            rigidbody2D.MovePosition(transform.position + dir * speed * Time.deltaTime);
        }
        else
        {
            if (Vector2.Distance(transform.position, A) < .1f) initialPosition = B;
            if (Vector2.Distance(transform.position, B) < .1f) initialPosition = A;

            animator.SetFloat("X", initialPosition.x);
            animator.SetFloat("Y", initialPosition.y);
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

    IEnumerator Attack(float seconds)
    {
        attacing = true;
        if (target != initialPosition && projectile != null)
        {
            target = player.transform.position;
            dir = (target - transform.position).normalized;
            GameObject gameObject = Instantiate(projectile, transform.position, transform.rotation);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x, dir.y) * 2f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(seconds);
        }
        attacing = false;
    }

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Attack")
        {
            animator.Play(destroyState);
            CheckDrop();
            speed = 0;
            yield return new WaitForSeconds(timeForDisable);

            foreach (Collider2D c in GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
        }
    }

    public void Attacked()
    {
        --hp;
    }

    public void CheckDrop()
    {
        if (dropList.Length > 0)
        {
            int rnd = (int)Random.Range(0, 100);

            foreach (var item in dropList)
            {
                if (item.chance < rnd)
                {
                    item.CreateDropItem(gameObject.transform.position);
                    return;
                }
            }
        }
    }
}
