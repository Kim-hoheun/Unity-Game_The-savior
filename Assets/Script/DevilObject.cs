using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilObject : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint; // �̸� �̵���ų ��ü
    public LayerMask whatStopMovement;
    public bool moveCheck;
    public Transform parentTransform;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        parentTransform = transform.parent;
        movePoint.parent = parentTransform;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)// ��ü�� Player��ü�� �Ÿ��� ���� ���� �������� �̵��� ����(���� �־����� ������ �̻�����)
        {
            if (moveCheck && Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) // �¿� ����Ű �Է�
            {

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopMovement))// �÷��̾ �̵��� ������ 0.2fũ���� ���� �׸��� �浿�� ��ü�� �ִٸ� true�� �����ؼ� ����ȉ�
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);                                               // �̵��� ������ �ƹ��͵� ���ٸ� �̵��� �������� ��ǥ�� ���� ���� �ش�.
                    moveCheck = false;
                }
                else
                {
                    Invoke("DevilDistroy", 0.5f);
                    moveCheck = false;
                }
                

            }
            else if (moveCheck && Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) // ���� ����Ű �Է�
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopMovement))// �÷��̾ �̵��� ������ 0.2fũ���� ���� �׸��� �浿�� ��ü�� �ִٸ� true�� �����ؼ� ����ȉ�
                {

                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);                                                // �̵��� ������ �ƹ��͵� ���ٸ� �̵��� �������� ��ǥ�� ���� ���� �ش�.
                    moveCheck = false;
                }
                else
                {
                    Invoke("DevilDistroy", 0.5f);
                    moveCheck = false;
                }
                

            }

        }
    }
    public void damagedAnimation()
    {
        anim.SetTrigger("damaged");
    }
    public void DevilMoveIs()
    {
        moveCheck = true;
    }
    public void DevilDistroy()
    {
        Destroy(gameObject);
    }
    //-> Devil Oject �����ϱ�
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // �Ǹ� ������Ʈ�� �÷��̾� ������ ������ �÷��̾ ������ ���� �������� ��ĭ ��ӵ� ����� �̵����� ������ ���Ѵ�
    // ���� �÷��̾��� ������ 2ȸ �޴� �ٸ� ������Ʈ�� �ı� �ȴ�.
    // ���� �÷��̾ ������ ���͹����� ������ ��ĭ�̶�� �̵��Ѵ�
    // ���� �÷��̾ ������ ���͹����� ������ ��ĭ�� �̰� ���� ������Ʈ�� �־ �̵��Ѵ�
    // ���� �÷��̾ ������ ���͹����� ������ ���� ���̰ų�, ���̶�� �̵����� �ʴ´�
    // ���� �÷��̾ ������ ���͹����� ������ ��ĭ�� �ƴ϶�� �̵����� �ʴ´�.

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}
