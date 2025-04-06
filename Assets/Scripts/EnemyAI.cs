using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    [Header("AI Settings")]
    public Transform player; // Цель для преследования
    public float detectionRange = 10f; // Дистанция обнаружения игрока
    public float attackRange = 2f; // Дистанция атаки
    public float attackCooldown = 1f; // Задержка между атаками
    public float wanderRadius = 5f; // Радиус блуждания
    public float wanderTimer = 5f; // Частота смены точки блуждания
    
    private NavMeshAgent agent;
    private float lastAttackTime;
    private float timer;
    private Vector3 startPosition;
    private bool isPlayerInRange;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        
        // Автоматически находим игрока если не установлен
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        isPlayerInRange = distanceToPlayer <= detectionRange;

        if (isPlayerInRange)
        {
            // Преследование игрока
            agent.SetDestination(player.position);

            // Атака если достаточно близко
            if (distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            // Режим блуждания
            timer += Time.deltaTime;
            if (timer >= wanderTimer)
            {
                Wander();
                timer = 0;
            }
        }
    }

    void Attack()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(10);
            Debug.Log($"{name} атаковал игрока!");
        }
    }

    void Wander()
    {
        // Генерируем случайную точку в радиусе блуждания
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += startPosition;
        
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
        agent.SetDestination(hit.position);
    }

    // Визуализация зон в редакторе
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

// public class EnemyAI : MonoBehaviour
// {
//     public Transform player; // Ссылка на игрока
//     private NavMeshAgent agent;
//     public float attackRange = 2f; // Дистанция атаки
//     public float attackCooldown = 1f; // Задержка между атаками
//     private float lastAttackTime;

//     void Start()
//     {
//         agent = GetComponent<NavMeshAgent>();
//         // if (player == null)
//         //     player = GameObject.FindGameObjectWithTag("Player").transform;
//     }

//     void Update(){
//     {

//         if (player != null)
//             agent.SetDestination(player.position);
//     }
//         // if (player == null) return;

//         // // Преследование игрока
//         // agent.SetDestination(player.position);

//         // Проверка дистанции для атаки
//         float distanceToPlayer = Vector3.Distance(transform.position, player.position);
//         if (distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
//         {
//             Attack();
//             lastAttackTime = Time.time;
//         }
//     }
//     void Attack()
//     {
//         PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
//         if (playerHealth != null)
//         {
//             playerHealth.TakeDamage(10); // Урон 100 единиц. Размер на все здоровье персонажа. Пока убивает с одного удара.
//         }
//     }
// }
