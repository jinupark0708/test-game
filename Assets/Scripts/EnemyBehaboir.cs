using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject enemyBullet;  // �߻��� �Ѿ� ������Ʈ
    public float fireInterval = 3.0f;  // �Ѿ� �߻� ����

    private Animator animator;
    private float fireDelay;          // �Ѿ� �߻� ������

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
        // �Ѿ� �߻� ������ üũ
        fireDelay += Time.deltaTime;
        if (fireDelay > fireInterval)
        {
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            fireDelay = 0.0f;
        }
    }
}
