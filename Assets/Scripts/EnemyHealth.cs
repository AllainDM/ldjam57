using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Метод для получения урона
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " получил " + damage + " урона. Осталось " + currentHealth + " HP.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Метод для смерти врага
    void Die()
    {
        Debug.Log(gameObject.name + " умер!");
        Destroy(gameObject); // Уничтожаем врага (можно заменить на анимацию)
    }
}
