using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    private NavMeshAgent agent;
    public float attackRange = 2f; // Дистанция атаки
    public float attackCooldown = 1f; // Задержка между атаками
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // if (player == null)
        //     player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update(){
    {

        if (player != null)
            agent.SetDestination(player.position);
    }
        // if (player == null) return;

        // // Преследование игрока
        // agent.SetDestination(player.position);

        // Проверка дистанции для атаки
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }
    void Attack()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(10); // Урон 100 единиц. Размер на все здоровье персонажа. Пока убивает с одного удара.
        }
    }
}
