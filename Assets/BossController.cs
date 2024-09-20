using UnityEngine;

public class BossController : MonoBehaviour
{
    public int maxHealth = 200;  // ボスの体力
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Boss takes damage: " + damage + ", remaining health: " + currentHealth);

        // 体力が0になったらボスを消す
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);  // ボスが消える
        Debug.Log("Boss defeated!");
    }
}
