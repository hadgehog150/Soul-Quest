using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beamos : MonoBehaviour
{
    public int hp;
    public float attackRadious;
    public float attackSpeed = 2f;
    public GameObject projectile;

    public string destroyState;
    public float timeForDisable;

    private GameObject player;
    private Animator animator;
    private Vector3 initialPosition, target, dir;
    private bool attacing;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        target = initialPosition;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, attackRadious, 1 << LayerMask.NameToLayer("Default"));

        Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                target = player.transform.position;
            }
        }

        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position).normalized;

        if (target != initialPosition && distance < attackRadious)
        {
            animator.SetFloat("X", dir.x);
            animator.SetFloat("Y", dir.y);
            animator.Play("walking_tree", -1, 0);

            if (!attacing) StartCoroutine(Attack(attackSpeed));
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
        Gizmos.DrawWireSphere(transform.position, attackRadious);
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
}
