using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject bossEnergyBallPrefab; // ボスのエネルギー弾のプレハブ
    public Transform firePoint1; // 1つ目の発射位置
    public Transform firePoint2; // 2つ目の発射位置
    public Transform player; // プレイヤーのTransform
    public float fireRate = 3f; // 発射間隔
    private float nextFireTime = 0f; // 次の発射時間

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // 次の発射時間を設定
        }
    }

    void Shoot()
    {
        // ボスのエネルギー弾を2つ生成
        GameObject energyBall1 = Instantiate(bossEnergyBallPrefab, firePoint1.position, firePoint1.rotation);
        GameObject energyBall2 = Instantiate(bossEnergyBallPrefab, firePoint2.position, firePoint2.rotation);

        // プレイヤーを追尾するために、それぞれのエネルギー弾のターゲットをプレイヤーに設定
        BossEnergyBall energyBall1Script = energyBall1.GetComponent<BossEnergyBall>();
        if (energyBall1Script != null)
        {
            energyBall1Script.SetTarget(player);
        }

        BossEnergyBall energyBall2Script = energyBall2.GetComponent<BossEnergyBall>();
        if (energyBall2Script != null)
        {
            energyBall2Script.SetTarget(player);
        }
    }
}
