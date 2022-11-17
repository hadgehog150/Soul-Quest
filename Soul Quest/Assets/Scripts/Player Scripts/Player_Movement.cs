using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
    public float speed, dashSpeed;

    public int hp;
    public int maxHp;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite zeroHeart;

    public Key key;

    private float x, y, activSpeed;
    private bool isWalking;
    private bool isProtecting;
    private Rigidbody2D rigidbody;
    private Animator anim;

    public bool jumping;//атакует не атакует

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        activSpeed = speed;
        hp = maxHp;
    }

    void Update() {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        jumping = stateInfo.IsName("jump_tree");

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        rigidbody.velocity = new Vector2(x * activSpeed, y * activSpeed);

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!isProtecting)
            {
                isProtecting = true;
                speed = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            if (isProtecting)
            {
                isProtecting = false;
                speed = 4;
                activSpeed = speed;
            }
        }

        if (DialogueManager.isActive)
        {
            activSpeed = 0;
            return;
        }
        else {
            activSpeed = speed;
        }

        if (hp > maxHp)
        {
            hp = maxHp;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < hp)
            {
                hearts[i].sprite = fullHeart;
            }
            else {
                hearts[i].sprite = zeroHeart;
            }

            if (i < maxHp)
            {
                hearts[i].enabled = true;
            }
            else {
                hearts[i].enabled = false;
            }
        }

        

        if (hp <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        if (x != 0 || y != 0) {
            Move();

            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!jumping)
                {
                    jumping = true;
                    anim.SetTrigger("jumping");//триггер
                    dashSpeed = 12;
                    speed = dashSpeed;
                    activSpeed = speed;
                }
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                jumping = false;
                float playBackTime = stateInfo.normalizedTime;
                dashSpeed = 4;
                speed = dashSpeed;
                activSpeed = speed;
            }

            if (!isWalking) {
                isWalking = true;
                x = 0;
                y = 0;
                anim.SetBool("isWalking", isWalking);
            }
        } else {
            if (isWalking) {
                isWalking = false;
                anim.SetBool("isWalking", isWalking);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Move() {
        anim.SetFloat("X", x);
        anim.SetFloat("Y", y);
    }

    public void Jump() {
        if (!jumping) {
            jumping = true;
            anim.SetTrigger("jumping");//триггер
            activSpeed = dashSpeed;
        }
    }

    public void Attacked()
    {
        if (--hp <=0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            --hp;
        }


        if (collision.tag == "HP")
        {
            Destroy(collision.gameObject);
            ++hp;
        }
    }
}
