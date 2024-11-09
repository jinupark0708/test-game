using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyController : MonoBehaviour
{
    public float knockbackDistance = 2.0f;  // �÷��̾� �о�� �Ÿ�
    public float moveSpeed = 2.0f;          // ���� �̵� �ӵ�
    public float boundaryDistance = 5.0f;   // �ʱ� ��ġ�κ��� �¿� �̵� ������ �Ÿ�

    private Animator animator;
    private bool onDead = false;            // ���Ͱ� ���� �������� Ȯ��
    private bool movingRight = true;        // ���� ���������� �̵� ������ ����
    private float leftBoundary;             // �ʱ� ��ġ ���� ���� ���
    private float rightBoundary;            // �ʱ� ��ġ ���� ������ ���

    void Start()
    {
        animator = GetComponent<Animator>();

        // �ʱ� ��ġ�� �������� �¿� ��� ����
        leftBoundary = transform.position.x - boundaryDistance;
        rightBoundary = transform.position.x + boundaryDistance;

        // Rigidbody2D�� �ִ��� Ȯ��
        if (GetComponent<Rigidbody2D>() == null)
        {
            gameObject.AddComponent<Rigidbody2D>().isKinematic = true; // Rigidbody2D�� ���� �� �߰��ϰ� Kinematic���� ����
        }
    }

    void Update()
    {
        if (onDead) return;  // ���� ���¿����� �ƹ� ���۵� ���� ����

        Move();              // ���� �̵�
        CheckBoundaries();   // �̵� ���� üũ
    }

    private void Move()
    {
        // �¿� �̵�
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }

    private void CheckBoundaries()
    {
        // ��踦 ����� ���� ��ȯ
        if (transform.position.x >= rightBoundary)
        {
            movingRight = false;
        }
        else if (transform.position.x <= leftBoundary)
        {
            movingRight = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾� �о��
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(knockbackDir * knockbackDistance, ForceMode2D.Impulse);
            }
        }
    }
}
