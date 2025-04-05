using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int _health = 100;

    public void TakeDamage(int damage)
    {
        _health -= damage;
        Debug.Log("Здоровье игрока: " + _health);

        if (_health <= 0) {
            // Destroy(gameObject);
            Debug.Log("Игрока погиб.");
            // Player.Instance.Die();
        }
    }

    public int GetHealth()
    {
        return _health;
    }
}