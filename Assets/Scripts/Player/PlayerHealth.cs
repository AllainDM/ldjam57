using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float respawnTime = 3f;
    public GameObject deathEffect;
    private int currentHealth;
    private bool isDead;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"Игрок инициализирован, здоровье: {currentHealth}/{maxHealth}");
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) 
        {
            Debug.Log("Игрок уже мертв!");
            return;
        }

        currentHealth -= damageAmount;
        Debug.Log($"Игрок получил {damageAmount} урона. Осталось: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("<color=red>Игрок умер!</color>");

        // Эффект смерти
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Отключаем управление
        GetComponent<Player>().enabled = false;
        
        // Запускаем респавн
        // Invoke(nameof(Respawn), respawnTime);
    }

    void Respawn()
    {
        currentHealth = maxHealth;
        isDead = false;
        
        // Включаем управление
        GetComponent<Player>().enabled = true;
        
        Debug.Log("<color=green>Игрок возродился!</color>");
        // Здесь можно добавить телепортацию к точке респавна
    }

    // Для отображения здоровья в UI
    public float GetHealthPercentage()
    {
        return (float)currentHealth / maxHealth;
    }
}
// public class PlayerHealth : MonoBehaviour
// {
//     private int _health = 100;

//     public void TakeDamage(int damage)
//     {
//         _health -= damage;
//         Debug.Log("Здоровье игрока: " + _health);

//         if (_health <= 0) {
//             // Destroy(gameObject);
//             Debug.Log("Игрока погиб.");
//             // Player.Instance.Die();
//         }
//     }

//     public int GetHealth()
//     {
//         return _health;
//     }
// }