using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public float knockbackDistance = 2.0f;  // 플레이어가 밀려날 거리
    float time;
    Rigidbody2D rg2D;
    GameObject player;

    private void Start()
    {
        moveSpeed = 10.0f;
        rotateSpeed = 300.0f;
        time = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        rg2D = GetComponent<Rigidbody2D>();
        FireBullet();
    }

    private void Update()
    {
        DestroyBullet();
        RotateBullet();
    }

    private void FireBullet()
    {
        Vector3 distance = player.transform.position - transform.position;
        Vector3 dir = distance.normalized;
        rg2D.velocity = dir * moveSpeed;
    }

    private void RotateBullet()
    {
        transform.rotation = Quaternion.Euler(0, 0, time * rotateSpeed);
    }

    private void DestroyBullet()
    {
        time += Time.deltaTime;
        if (time > 5.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player 충돌 감지");  // 디버그 메시지 추가
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(knockbackDir * knockbackDistance, ForceMode2D.Impulse);
            }
            Destroy(gameObject);  // 총알 삭제
        }
    }
}
