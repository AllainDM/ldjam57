using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float destroyDelay = 2f;
    public GameObject hitEffect;
    public GameObject deathEffect;

    private int currentHealth;
    private bool isDead;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"{name} инициализирован, здоровье: {currentHealth}/{maxHealth}");
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) 
        {
            Debug.Log($"{name} уже мертв!");
            return;
        }

        currentHealth -= damageAmount;
        Debug.Log($"{name} получил {damageAmount} урона. Осталось: {currentHealth}/{maxHealth}");

        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position + Vector3.up, Quaternion.identity);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log($"<color=red>{name} УМЕР!</color>");

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        GetComponent<Collider>().enabled = false;
        var ai = GetComponent<EnemyAI>();
        if (ai != null) ai.enabled = false;

        Destroy(gameObject, destroyDelay);
    }
}

// public class EnemyHealth : MonoBehaviour
// {
//     public int maxHealth = 100;
//     private int currentHealth;

//     void Start()
//     {
//         currentHealth = maxHealth;
//     }

//     // Метод для получения урона
//     public void TakeDamage(int damage)
//     {
//         currentHealth -= damage;
//         Debug.Log(gameObject.name + " получил " + damage + " урона. Осталось " + currentHealth + " HP.");

//         if (currentHealth <= 0)
//         {
//             Die();
//         }
//     }

//     // Метод для смерти врага
//     void Die()
//     {
//         Debug.Log(gameObject.name + " умер!");
//         Destroy(gameObject); // Уничтожаем врага (можно заменить на анимацию)
//     }
// }
