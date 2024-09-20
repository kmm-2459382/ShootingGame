using UnityEngine;

public class AlienController : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public int damage = 10;      // プレイヤーに与えるダメージ
    public float speed = 5f;     // Alienの移動速度
    public float rotationSpeed = 5f; // 回転速度
    public float stoppingDistance = 1f;  // プレイヤーにどれくらい接近するか

    private Transform player;
    private PlayerHealth playerHealth;

    void Start()
    {
        // "Player" タグが付いたオブジェクトの Transform を取得
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        // プレイヤーが見つかった場合、参照を取得
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerHealth = playerObject.GetComponent<PlayerHealth>();
        }
        else
        {
            Debug.LogError("Player object not found! Make sure the player has the 'Player' tag.");
        }

        // 現在の体力を初期化
        currentHealth = maxHealth;
    }

    void Update()
    {
        // プレイヤーが存在する場合に移動
        if (player != null)
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

    void OnTriggerEnter(Collider other)
    {
        // プレイヤーに当たった場合の処理
        if (other.CompareTag("Player"))
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);  // プレイヤーにダメージを与える
            }

            Destroy(gameObject);  // 自身(Alien)を破壊
        }
    }

    // ダメージを受けたときの処理
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 体力が0になったときの処理
    void Die()
    {
        Destroy(gameObject);  // 自身(Alien)を破壊
        Debug.Log("Alien destroyed!");
    }
}
