using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Animator anim;
    private bool attacking;//������� �� �������

    void Update()//��� ��� ��� ����� ��� ��� �� �������� �� ����������� ���� �� ����������
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        attacking = stateInfo.IsName("sword_tree");

        if (attacking)
        {
            float playBackTime = stateInfo.normalizedTime;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attacking();
        }
    }

    public void Attacking()
    {
        if (!attacking)
        {
            anim.SetTrigger("attacking");//�������
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.SendMessage("Attacked");
        }
    }
}
