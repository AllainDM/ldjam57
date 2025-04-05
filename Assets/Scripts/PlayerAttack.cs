using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    public int damage = 10; // Урон за удар
    public float attackRange = 2f; // Дистанция атаки
    public LayerMask enemyLayer; // Слой врагов

    void Update()
    {
        if (Input.GetMouseButtonDown(2)) // ЛКМ
        {
            Attack();
        }
    }

    void Attack()
    {
        // Проверяем, есть ли враг перед игроком
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, enemyLayer))
        {
            // Если попали во врага
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log("Игрок атаковал " + hit.collider.name);
            }
        }
    }
}
