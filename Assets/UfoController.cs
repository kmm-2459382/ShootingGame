using UnityEngine;

public class UfoController : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public int damage = 10;      // プレイヤーに与えるダメージ
    public float speed = 5f;     // UFOの移動速度

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
            // プレイヤーへの方向を計算
            Vector3 direction = (player.position - transform.position).normalized;

            // プレイヤーの方向に向けて回転させる
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);

            // UFOをプレイヤーの方向に向けて移動させる
            transform.position += transform.forward * speed * Time.deltaTime;
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

            Destroy(gameObject);  // 自身(UFO)を破壊
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
        Destroy(gameObject);  // 自身(UFO)を破壊
        Debug.Log("UFO destroyed!");
    }
}
