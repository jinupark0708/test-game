using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject enemyBullet;  // 발사할 총알 오브젝트
    public float fireInterval = 3.0f;  // 총알 발사 간격

    private Animator animator;
    private float fireDelay;          // 총알 발사 딜레이

    void Start()
    {
        animator = GetComponent<Animator>();
        fireDelay = 0.0f;
    }

    void Update()
    {
        FireBullet();
    }

    public void FireBullet()
    {
        // 총알 발사 딜레이 체크
        fireDelay += Time.deltaTime;
        if (fireDelay > fireInterval)
        {
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            fireDelay = 0.0f;
        }
    }
}
