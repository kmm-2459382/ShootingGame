using UnityEngine;

public class PlayerUltimateAttack : MonoBehaviour
{
    public GameObject energyBallPrefab;  // エネルギーボールのPrefab
    public Transform firePoint;          // エネルギーボールの発射位置
    public float shootInterval = 5f;     // 必殺技の発射間隔（秒）

    private float nextShootTime = 0f;    // 次にエネルギーボールを発射できる時間

    void Update()
    {
        // スペースキーが押されており、次の発射時間が来ている場合
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextShootTime)
        {
            ShootEnergyBall();
            nextShootTime = Time.time + shootInterval;  // 次の発射時間を設定
        }
    }

    void ShootEnergyBall()
    {
        // エネルギーボールを発射する
        if (energyBallPrefab != null && firePoint != null)
        {
            Instantiate(energyBallPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            Debug.LogWarning("Energy Ball Prefab or Fire Point not assigned.");
        }
    }
}
