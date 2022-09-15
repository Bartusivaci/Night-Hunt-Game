using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat instance;

    void Awake()
    {
        instance = this;
    }


    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public LayerMask projectileLayers;

    public int damage = 100;

    public void HitEnemies()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Collider2D[] hitProjectiles = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, projectileLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }

        foreach (Collider2D projectile in hitProjectiles)
        {
            Destroy(projectile.gameObject);
        }
    }


    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
