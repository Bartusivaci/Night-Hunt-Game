using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public float walkSpeed = 2f;
    public int maxHealth = 100;
    public Transform player;
    public float agroRange = 3f; 

    private int currentHealth;
    private Animator animator;
    

    private const int DEAD_LAYER = 8;


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
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }
}
