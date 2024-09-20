using UnityEngine;

public class BossEnergyBall : MonoBehaviour
{
    public float speed = 40f; // エネルギー弾の速度
    private Transform target; // 追従するターゲット
    private bool isTracking = false; // プレイヤー追従の状態
    public float lifetime = 7f; // エネルギー弾が存在する時間（秒）
    public int damage = 5; // エネルギー弾が与えるダメージ

    private Transform player;
    private PlayerHealth playerHealth;

    void Start()
    {
        // 一定時間後にエネルギー弾を自動的に消去する
        Destroy(gameObject, lifetime);
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

            // BossEnergyBallをプレイヤーの方向に向けて移動させる
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    // ターゲットを設定するメソッド
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        isTracking = true; // ターゲットが設定されたら追従を開始する
    }

    // エネルギー弾が衝突したときの処理
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerEnergyBall") || other.CompareTag("RainbowPlayerEnergyBall"))
        {
            // プレイヤーがダメージを受ける
            if (other.CompareTag("Player"))
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }

            // エネルギー弾を消去
            Destroy(gameObject);
        }
    }
}
