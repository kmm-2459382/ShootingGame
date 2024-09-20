using UnityEngine;

public class FirePointSetup : MonoBehaviour
{
    public GameObject boss; // ボスのゲームオブジェクト
    public Transform firePoint1; // 1つ目の発射点
    public Transform firePoint2; // 2つ目の発射点

    void Start()
    {
        // ボスの位置を基準に発射点の位置を設定
        if (firePoint1 != null && firePoint2 != null && boss != null)
        {
            Vector3 bossPosition = boss.transform.position;

            // firePoint1 の位置をボスの位置に基づいて設定
            firePoint1.position = new Vector3(bossPosition.x + 6.5f, bossPosition.y - 0.48f, bossPosition.z - 0.4f);

            // firePoint2 の位置をボスの位置に基づいて設定
            firePoint2.position = new Vector3(bossPosition.x - 6.5f, bossPosition.y - 0.48f, bossPosition.z - 0.4f);
        }
    }
}
