using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    public int damage = 10;
    
    public float attackRange = 2f;
    
    public float attackWidth = 0.5f;
    
    public LayerMask enemyLayer;
    
    public float attackCooldown = 1f;
    
    public Color hitColor = Color.red;
    
    public Color missColor = Color.yellow;
    
    
    public float debugDrawTime = 1f;

    private float nextAttackTime; // Время следующей возможной атаки
    private bool lastAttackHit; // Попала ли последняя атака

    void Update()
    {
        // Проверяем нажатие левой кнопки мыши
        if (Input.GetMouseButtonDown(0))
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        // 1. ПРОВЕРКА КУЛДАУНА
        if (Time.time < nextAttackTime)
        {
            Debug.Log($"Атака на перезарядке! Доступна через {nextAttackTime - Time.time:0.00} сек");
            return;
        }

        // 2. ОСНОВНАЯ ЛОГИКА АТАКИ
        bool hitRegistered = PerformAttack();

        // 3. ВИЗУАЛЬНАЯ ОБРАТНАЯ СВЯЗЬ
        if (hitRegistered)
        {
            Debug.Log("<color=green>Попадание!</color> Враг получил урон");
            // Здесь можно добавить эффекты попадания (например, вспышку)
        }
        else
        {
            Debug.Log("<color=yellow>Промах!</color> Враги не обнаружены в радиусе атаки");
            // Здесь можно добавить эффекты промаха
        }

        // 4. ОБНОВЛЕНИЕ ТАЙМЕРОВ
        nextAttackTime = Time.time + attackCooldown;
        lastAttackHit = hitRegistered;
    }

    bool PerformAttack()
    {
        // Рассчитываем параметры атаки
        Vector3 attackStart = transform.position + Vector3.up * 0.5f; // Старт от груди персонажа
        Vector3 attackDirection = transform.forward;
        bool hitEnemy = false;

        // 1. ПОИСК ВРАГОВ В ЗОНЕ АТАКИ (SphereCast)
        RaycastHit[] hits = Physics.SphereCastAll(
            attackStart,
            attackWidth,
            attackDirection,
            attackRange,
            enemyLayer
        );

        // 2. ОБРАБОТКА ПОПАДАНИЙ
        foreach (RaycastHit hit in hits)
        {
            // Проверяем, есть ли у объекта компонент EnemyHealth
            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                // Наносим урон
                enemy.TakeDamage(damage);
                hitEnemy = true;
                
                // Визуализация попадания
                Debug.DrawLine(attackStart, hit.point, hitColor, debugDrawTime);
                Debug.DrawRay(hit.point, Vector3.up * 2f, hitColor, debugDrawTime); // Маркер попадания
            }
        }

        // 3. ВИЗУАЛИЗАЦИЯ АТАКИ
        Debug.DrawRay(attackStart, attackDirection * attackRange, 
            hitEnemy ? hitColor : missColor, debugDrawTime);
        
        // Визуализация ширины атаки
        DrawAttackCone(attackStart, attackDirection);

        return hitEnemy;
    }

    // Рисуем конус атаки для наглядности
    void DrawAttackCone(Vector3 center, Vector3 direction)
    {
        Vector3 perpendicular = Vector3.Cross(direction, Vector3.up).normalized * attackWidth;
        Vector3 end = center + direction * attackRange;
        
        Debug.DrawLine(center, end + perpendicular, missColor, debugDrawTime);
        Debug.DrawLine(center, end - perpendicular, missColor, debugDrawTime);
    }

    // Отображаем зону атаки в редакторе
    void OnDrawGizmosSelected()
    {
        Vector3 attackStart = transform.position + Vector3.up * 0.5f;
        Vector3 attackEnd = attackStart + transform.forward * attackRange;

        // Линия атаки
        Gizmos.color = lastAttackHit ? hitColor : missColor;
        Gizmos.DrawLine(attackStart, attackEnd);

        // Сфера на конце атаки
        Gizmos.color = new Color(hitColor.r, hitColor.g, hitColor.b, 0.3f);
        Gizmos.DrawSphere(attackEnd, attackWidth);
    }
}
// public class PlayerAttack : MonoBehaviour
// {
//     public int damage = 10; // Урон за удар
//     public float attackRange = 2f; // Дистанция атаки
//     public LayerMask enemyLayer; // Слой врагов

//     private void Update()
//     {
//         if (Input.GetMouseButtonDown(2)) // ЛКМ
//         {
//             Attack();
//         }
//     }

//     private void Attack()
//     {
//         GetComponent<Animator>().SetBool("IsAttack", true);
//         // Проверяем, есть ли враг перед игроком
//         RaycastHit hit;
//         if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, enemyLayer))
//         {
//             // Если попали во врага
//             EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
//             if (enemyHealth != null)
//             {
//                 enemyHealth.TakeDamage(damage);
//                 Debug.Log("Игрок атаковал " + hit.collider.name);
//             }
//         }

//         StartCoroutine(EndAttack());
//     }

//     IEnumerator EndAttack()
//     {
//         yield return new WaitForSeconds(1);

//         GetComponent<Animator>().SetBool("IsAttack", false);
//     }
// }
