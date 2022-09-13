using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator animator;

    private bool isGrounded;

    public float jumpForce = 6f;
    public int attackCombo = 1;
    public bool isAttacking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        AttackCombo();

        /*
        if(Input.GetKeyDown(KeyCode.Q) && isGrounded)
        {
            animator.SetTrigger("Attack1");
            SoundManager.instance.PlaySFX("Attack1");
        }

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            animator.SetTrigger("Attack2");
            SoundManager.instance.PlaySFX("Attack2");
        }

        if (Input.GetKeyDown(KeyCode.E) && isGrounded)
        {
            animator.SetTrigger("Attack3");
            SoundManager.instance.PlaySFX("Attack3");
        }
        */

        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    void FixedUpdate()
    {
        GroundCheck();
    }

    void GroundCheck()
    {
        isGrounded = false;

        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isGrounded = true;
        }

        animator.SetBool("Jump", !isGrounded);
    }


    void Jump()
    {
        Vector2 velocity = rb.velocity;
        velocity.y = jumpForce;
        rb.velocity = velocity;

        animator.SetBool("Jump", true);
    }

    public void AttackCombo()
    {
        if(Input.GetKeyDown(KeyCode.E) && !isAttacking && isGrounded)
        {
            isAttacking = true;
            animator.SetTrigger("Attack" + attackCombo);
            SoundManager.instance.PlaySFX("Attack" + attackCombo);
        }
    }

    public void StartCombo()
    {
        isAttacking = false;
        if(attackCombo < 4)
        {
            attackCombo++;
        }
    }

    public void FinishCombo()
    {
        isAttacking = false;
        attackCombo = 1;
    }
}
