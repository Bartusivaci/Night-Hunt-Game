using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EZCameraShake;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator animator;

    private bool isGrounded;

    public float jumpForce = 6f;
    public int attackCombo = 1;
    public bool isAttacking;
    public bool isJumpAttacking;

    public int maxHealth = 3;
    private int currentHealth;

    public SpriteRenderer[] livesSprites;
    public Sprite emptyHeart;

    private bool isAlive;
    public float dragSpeed = 2.5f;
    public float sceneLoadDelay = 3f;

    private const int DEAD_LAYER = 8;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        isAlive = true;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && isAlive)
        {
            Jump();
        }

        if(rb.velocity.y == 0f)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", false);
        }

        if(rb.velocity.y > 0.001f && !isJumpAttacking)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Fall", false);
        }

        if (rb.velocity.y < 0f && !isJumpAttacking)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", true);
        }

        AttackCombo();

        if(Input.GetKeyDown(KeyCode.C) && isGrounded && isAlive)
        {
            Crouch();
        }

        if (Input.GetKeyDown(KeyCode.R) && isGrounded && !isAttacking && isAlive)
        {
            animator.SetTrigger("Roll");
        }

        if(Input.GetKeyDown(KeyCode.E) && !isGrounded && !isAttacking && isAlive)
        {
            isJumpAttacking = true;
            isAttacking = true;
            animator.SetBool("JumpAttack", true);
            SoundManager.instance.PlaySFX("Attack2");
            PlayerCombat.instance.HitEnemies();
            Invoke("StopJumpAttacking", animator.GetCurrentAnimatorStateInfo(0).length);
        }

        if (!isAlive)
        {
            transform.Translate(Vector2.left * Time.deltaTime * dragSpeed);
        }

        //animator.SetFloat("yVelocity", rb.velocity.y);
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
            animator.SetBool("Fall", false);
        }

        //animator.SetBool("Jump", !isGrounded);
    }


    void Jump()
    {
        Vector2 velocity = rb.velocity;
        velocity.y = jumpForce;
        rb.velocity = velocity;

        //animator.SetBool("Jump", true);
    }

    void StopJumpAttacking()
    {
        isJumpAttacking = false;
        isAttacking = false;
        animator.SetBool("JumpAttack", false);
    }

    void Crouch()
    {
        animator.SetTrigger("Crouch");
        SoundManager.instance.PlaySFX("Crouch");
    }

    public void AttackCombo()
    {
        if(Input.GetKeyDown(KeyCode.E) && !isAttacking && isGrounded && isAlive)
        {
            isAttacking = true;
            animator.SetTrigger("Attack" + attackCombo);
            SoundManager.instance.PlaySFX("Attack" + attackCombo);
            PlayerCombat.instance.HitEnemies();
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


    public void TakeDamage()
    {
        if (isAlive)
        {
            animator.SetTrigger("Take Damage");
            SoundManager.instance.PlaySFX("DamageHitSound");
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);

            currentHealth--;
            livesSprites[currentHealth].sprite = emptyHeart;
        }

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            isAlive = false;
            animator.SetTrigger("Death");
            gameObject.layer = DEAD_LAYER;
            StartCoroutine(SceneLoadWithDelay());
        }
    }


    IEnumerator SceneLoadWithDelay()
    {
        yield return new WaitForSeconds(sceneLoadDelay);
        SceneManager.LoadScene("Menu Scene");
    }
}
