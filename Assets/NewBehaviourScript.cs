/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public float superJumpPower;
    public float hyperJumpPower;
    private SpriteRenderer spriteRenderer;
    Animator anim;
    Rigidbody2D rigid;

    private float jumpPressTime = 0f;
    private float superJumpThreshold = 0.5f;
    private float hyperJumpThreshold = 1.0f;
    private bool isJumping = false;
    private bool isSuperJump = false;
    private bool isHyperJump = false;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Jump Pressing
        if (Input.GetButton("Jump") && !isJumping)
        {
            jumpPressTime += Time.deltaTime;
        }

        // Jump Release
        if (Input.GetButtonUp("Jump") && !isJumping)
        {
            if (jumpPressTime >= hyperJumpThreshold)
            {
                // Hyper Jump
                rigid.AddForce(Vector2.up * hyperJumpPower, ForceMode2D.Impulse);
                isHyperJump = true;
            }
            else if (jumpPressTime >= superJumpThreshold)
            {
                // Super Jump
                rigid.AddForce(Vector2.up * superJumpPower, ForceMode2D.Impulse);
                isSuperJump = true;
            }
            else
            {
                // Normal Jump
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }

            anim.SetBool("Jumping", true);
            isJumping = true;
            jumpPressTime = 0f;
        }

        // Stop speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        // Direction sprite
        float h = Input.GetAxisRaw("Horizontal");
        if (h != 0)
        {
            spriteRenderer.flipX = h == -1;
        }

        // �ִϸ��̼� ������Ʈ
        if (Mathf.Abs(rigid.velocity.x) < 0.1f)
            anim.SetBool("Running", false);
        else
            anim.SetBool("Running", true);
    }

    private void FixedUpdate()
    {
        // ���� �ִϸ��̼� ���� Ȯ��
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool isPlayingDuckAnimation = stateInfo.IsName("Duck Animation");

        // �¿� �̵� (Duck Animation ��� ���� �ƴ� ���� �̵� ����)
        if (!isPlayingDuckAnimation)
        {
            float h = Input.GetAxisRaw("Horizontal");
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            // �ӵ� ����
            if (rigid.velocity.x > maxSpeed)
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < -maxSpeed)
                rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
        }

        // Landing platform
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null && rayHit.distance < 0.5f)
            {
                if (isJumping)
                {
                    anim.SetBool("Jumping", false);

                    // �������� �Ǵ� ������������ ���� ���� �ִϸ��̼� ���
                    if (isSuperJump || isHyperJump)
                    {
                        anim.SetBool("Landing", true);
                        StartCoroutine(PlayLandingAnimation());
                    }

                    isJumping = false;
                    isSuperJump = false;
                    isHyperJump = false;
                }
            }
        }
    }

    // ���� �ִϸ��̼� ����� ���� �ڷ�ƾ �߰�
    private IEnumerator PlayLandingAnimation()
    {
        anim.Play("Duck Animation"); // "Duck" �ִϸ��̼� ���� ���
        yield return new WaitForSeconds(0.2f); // ���� �ִϸ��̼� ��� �ð�
        anim.SetBool("Landing", false);
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public float superJumpPower;
    public float hyperJumpPower;
    public float fallThreshold = 2.0f; // ���� �ִϸ��̼� ����� ���� �Ÿ� ����
    private SpriteRenderer spriteRenderer;
    Animator anim;
    Rigidbody2D rigid;

    private float jumpPressTime = 0f;
    private float superJumpThreshold = 0.5f;
    private float hyperJumpThreshold = 1.0f;
    private bool isJumping = false;
    private bool isSuperJump = false;
    private bool isHyperJump = false;
    private bool isFalling = false;
    private float fallStartY;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Jump Pressing
        if (Input.GetButton("Jump") && !isJumping)
        {
            jumpPressTime += Time.deltaTime;
        }

        // Jump Release
        if (Input.GetButtonUp("Jump") && !isJumping)
        {
            if (jumpPressTime >= hyperJumpThreshold)
            {
                rigid.AddForce(Vector2.up * hyperJumpPower, ForceMode2D.Impulse);
                isHyperJump = true;
            }
            else if (jumpPressTime >= superJumpThreshold)
            {
                rigid.AddForce(Vector2.up * superJumpPower, ForceMode2D.Impulse);
                isSuperJump = true;
            }
            else
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }

            anim.SetBool("Jumping", true);
            isJumping = true;
            isFalling = false;
            jumpPressTime = 0f;
            fallStartY = rigid.position.y;
        }

        // Stop speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        // Direction sprite
        float h = Input.GetAxisRaw("Horizontal");
        if (h != 0)
        {
            spriteRenderer.flipX = h == -1;
        }

        // �ִϸ��̼� ������Ʈ
        if (Mathf.Abs(rigid.velocity.x) < 0.1f)
            anim.SetBool("Running", false);
        else
            anim.SetBool("Running", true);
    }

    private void FixedUpdate()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool isPlayingDuckAnimation = stateInfo.IsName("Duck Animation");

        // �¿� �̵� ���� (���� �ִϸ��̼� ���� �ƴ� ���� �̵� ����)
        if (!isPlayingDuckAnimation)
        {
            float h = Input.GetAxisRaw("Horizontal");
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            if (rigid.velocity.x > maxSpeed)
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < -maxSpeed)
                rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
        }

        // ���� ���� ���� (���� ���� �ƴ� ����)
        if (rigid.velocity.y < 0 && !isJumping && !isFalling)
        {
            isFalling = true;
            fallStartY = rigid.position.y;
        }

        // ���� ����
        if (rigid.velocity.y <= 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null && rayHit.distance < 0.5f)
            {
                if (isJumping || isFalling)
                {
                    anim.SetBool("Jumping", false);

                    float fallDistance = fallStartY - rigid.position.y;

                    // ���� �ִϸ��̼� ����
                    if (isSuperJump || isHyperJump || fallDistance > fallThreshold)
                    {
                        anim.SetBool("Landing", true);
                        StartCoroutine(PlayLandingAnimation());
                    }

                    isJumping = false;
                    isFalling = false;
                    isSuperJump = false;
                    isHyperJump = false;
                }
            }
        }
    }

    // ���� �ִϸ��̼� ����� ���� �ڷ�ƾ �߰�
    private IEnumerator PlayLandingAnimation()
    {
        anim.Play("Duck Animation");
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Landing", false);
    }
}