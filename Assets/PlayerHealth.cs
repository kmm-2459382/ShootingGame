using UnityEngine;
using UnityEngine.UI; // UI 名前空間を使用

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // プレイヤーの最大体力
    public int currentHealth;

    public Image healthBarFill; // 体力ゲージの `Image` コンポーネント

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            // 体力バーのサイズを体力に応じて調整
            float healthPercentage = (float)currentHealth / maxHealth;
            healthBarFill.fillAmount = healthPercentage;
        }
    }

    void Die()
    {
        Debug.Log("Player has died!");
        // プレイヤーが死んだときの処理（例：ゲームオーバー画面の表示など）
    }
}
