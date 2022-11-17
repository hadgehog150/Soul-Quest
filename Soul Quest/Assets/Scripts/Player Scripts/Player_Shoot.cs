using UnityEngine;

public class Player_Shoot : MonoBehaviour
{
    public Animator anim;
    private bool shooting;

    public GameObject projectile;

    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        shooting = stateInfo.IsName("shoot_tree");

        if (shooting)
        {
            float playBackTime = stateInfo.normalizedTime;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Shooting();
        }
    }

    public void Shooting()
    {
        if (!shooting)
        {
            anim.SetTrigger("shooting");
            GameObject gameObject = Instantiate(projectile, transform.position, transform.rotation);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(anim.GetFloat("X"), anim.GetFloat("Y"), 0f) * 10f, ForceMode2D.Impulse);
        }
    }
}
