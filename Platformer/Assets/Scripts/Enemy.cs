using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public float walkSpeed = 2f;
    public int maxHealth = 100;
    public Transform player;
    public float agroRange = 3f;

    public Transform firePosition;
    public GameObject projectile;


    private int currentHealth;
    private Animator animator;
    private bool isProjectileThrown = false;
    private bool isDamageGiven = false;
    

    private const int DEAD_LAYER = 8;
    private const int PLAYER_LAYER = 9;


    void Start()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * walkSpeed);

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if(distanceToPlayer < agroRange)
        {
            Attack();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    void HandleDeath()
    {
        animator.SetTrigger("Death");
        SoundManager.instance.PlaySFX("SkeletonDying");
        gameObject.layer = DEAD_LAYER;
    }

    void Attack()
    {
        if(transform.position.x > player.position.x)
        {
            animator.SetBool("Attack", true);
            if (!isProjectileThrown)
            {
                Instantiate(projectile, firePosition.position, firePosition.rotation);
                isProjectileThrown = true;
            }          
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == PLAYER_LAYER)
        {
            if (!isDamageGiven)
            {
                collision.collider.GetComponent<Player>().TakeDamage();
                isDamageGiven = true;
                HandleDeath();
            }
        }
    }
}
