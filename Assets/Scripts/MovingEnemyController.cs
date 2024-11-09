using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyController : MonoBehaviour
{
    public float knockbackDistance = 2.0f;  // 플레이어 밀어내기 거리
    public float moveSpeed = 2.0f;          // 몬스터 이동 속도
    public float boundaryDistance = 5.0f;   // 초기 위치로부터 좌우 이동 가능한 거리

    private Animator animator;
    private bool onDead = false;            // 몬스터가 죽은 상태인지 확인
    private bool movingRight = true;        // 현재 오른쪽으로 이동 중인지 여부
    private float leftBoundary;             // 초기 위치 기준 왼쪽 경계
    private float rightBoundary;            // 초기 위치 기준 오른쪽 경계

    void Start()
    {
        animator = GetComponent<Animator>();

        // 초기 위치를 기준으로 좌우 경계 설정
        leftBoundary = transform.position.x - boundaryDistance;
        rightBoundary = transform.position.x + boundaryDistance;

        // Rigidbody2D가 있는지 확인
        if (GetComponent<Rigidbody2D>() == null)
        {
            gameObject.AddComponent<Rigidbody2D>().isKinematic = true; // Rigidbody2D가 없을 시 추가하고 Kinematic으로 설정
        }
    }

    void Update()
    {
        if (onDead) return;  // 죽은 상태에서는 아무 동작도 하지 않음

        Move();              // 몬스터 이동
        CheckBoundaries();   // 이동 범위 체크
    }

    private void Move()
    {
        // 좌우 이동
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
        // 경계를 벗어나면 방향 전환
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
            // 플레이어 밀어내기
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(knockbackDir * knockbackDistance, ForceMode2D.Impulse);
            }
        }
    }
}
