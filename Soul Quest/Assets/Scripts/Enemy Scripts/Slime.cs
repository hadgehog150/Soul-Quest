using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour
{
    public Vector2 A, B;
    public float speed;
    public int hp;

    public float visionRadious;

    public string destroyState;
    public float timeForDisable;

    private GameObject player;
    private Rigidbody2D rigidbody2D;
    private Animator anim;
    private bool angry;
    private Vector3 initialPosition;

    private bool attacing;
    public float attackSpeed;

    public DropItem[] dropList;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        initialPosition = A;
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
        Vector3 dir = target - transform.position;

        if (angry)
        {
            angry = false;
            rigidbody2D.MovePosition(transform.position + dir * speed * Time.deltaTime);
        }
        else
        {
            if (Vector2.Distance(transform.position, A) < .1f) initialPosition = B;
            if (Vector2.Distance(transform.position, B) < .1f) initialPosition = A;

            transform.position = Vector2.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
        }
        
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

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
            anim.Play(destroyState);
            speed = 0;
            CheckDrop();
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
