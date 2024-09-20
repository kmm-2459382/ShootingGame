using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 500;            // ボスの最大体力
    public int currentHealth;             // 現在の体力
    public Image healthBarFill;           // HPバーのFill部分のImage
    public RectTransform healthBarCanvas; // HPバーを含むCanvasのRectTransform
    public Camera mainCamera;             // MainCamera
    public Camera subCamera;              // SubCamera

    public delegate void BossDeathHandler();  // ボス死亡時のイベント
    public event BossDeathHandler OnBossDeath;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // HPバーがカメラの方向を向くように調整
        if (mainCamera != null && subCamera != null)
        {
            // MainCameraとSubCamera両方に向ける
            Vector3 directionToMainCamera = mainCamera.transform.position - healthBarCanvas.position;
            Vector3 directionToSubCamera = subCamera.transform.position - healthBarCanvas.position;
            Vector3 combinedDirection = (directionToMainCamera + directionToSubCamera).normalized;
            healthBarCanvas.forward = combinedDirection;
        }
        else if (mainCamera != null)
        {
            // MainCameraだけに向ける
            healthBarCanvas.LookAt(mainCamera.transform);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Boss takes damage: " + damage + ", remaining health: " + currentHealth);

        // 体力が0以下になった場合の処理
        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthBar();
    }

    void Die()
    {
        // ボスが倒れた時の処理
        Debug.Log("Boss has been defeated.");
        OnBossDeath?.Invoke();  // ボスが倒れたことを通知
        Destroy(gameObject);    // ボスを消滅させる
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}
