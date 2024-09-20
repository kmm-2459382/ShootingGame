using UnityEngine;

public class AlienMovement : MonoBehaviour
{
    public Transform player;       // プレイヤーのTransform
    public float speed = 5f;       // 移動速度
    public float rotationSpeed = 5f; // 回転速度
    public float stoppingDistance = 1f;  // プレイヤーにどれくらい接近するか

    private void Update()
    {
        // プレイヤーとの距離を計算
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // プレイヤーに接近する
        if (distanceToPlayer > stoppingDistance)
        {
            // プレイヤーの方向を計算
            Vector3 direction = (player.position - transform.position).normalized;

            // 回転する
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            // 移動する
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
