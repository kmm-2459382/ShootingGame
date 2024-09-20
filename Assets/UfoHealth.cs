using UnityEngine;
using UnityEngine.UI;

public class UfoHealth : MonoBehaviour
{
    public int maxHealth = 15;            // UFOの最大体力を15に設定
    public int currentHealth;             // 現在の体力
    public Image healthBarFill;           // HPバーのFill部分のImage
    public RectTransform healthBarCanvas; // HPバーを含むCanvasのRectTransform
    public Camera mainCamera;             // MainCamera
    public Camera subCamera;              // SubCamera

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // カメラの方向を取得してHPバーを向ける
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
        Debug.Log("Ufo health: " + currentHealth);

        // 体力が0以下になった場合の処理
        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthBar();
    }

    void Die()
    {
        // UFOが倒れた時の処理
        Debug.Log("Ufo has been defeated.");
        Destroy(gameObject);  // UFOを消滅させる
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}
