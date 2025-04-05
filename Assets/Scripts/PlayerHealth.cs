using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Здоровье игрока: " + health);
        if (health <= 0) {
            // Destroy(gameObject);
            Debug.Log("Игрока погиб.");
            


        }
            

    }
}