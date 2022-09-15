using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 2.5f;
    public float projectileDestroyTime = 1f;

    private const int PLAYER_LAYER = 9;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        SoundManager.instance.PlaySFX("SwordFlying");
    }

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * projectileSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == PLAYER_LAYER)
        {
            animator.SetTrigger("hitPlayer");
            collision.GetComponent<Player>().TakeDamage();
            Destroy(gameObject, projectileDestroyTime);
        }
    }
}
