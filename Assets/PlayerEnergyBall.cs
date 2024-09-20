using UnityEngine;

public class PlayerEnergyBall : MonoBehaviour
{
    public float speed = 40f;  // エネルギー弾の速度

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * speed;
        }
        else
        {
            Debug.LogError("Rigidbody component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        // エネルギー弾が一定の z 座標以上に移動したら自動的に消滅
        if (transform.position.z >= 100f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 当たったオブジェクトのタグが"Ufo"、"Boss"、"Alien"、または"BossEnergyBall"
        if (other.CompareTag("Ufo"))
        {
            other.GetComponent<UfoHealth>()?.TakeDamage(5);  // ダメージを与える
            Destroy(gameObject);  // 弾を消す
        }
        else if (other.CompareTag("Boss"))
        {
            other.GetComponent<BossHealth>()?.TakeDamage(5);  // ダメージを与える
            Destroy(gameObject);  // 弾を消す
        }
        else if (other.CompareTag("Alien"))
        {
            other.GetComponent<AlienHealth>()?.TakeDamage(5);  // ダメージを与える
            Destroy(gameObject);  // 弾を消す
        }
        else if (other.CompareTag("BossEnergyBall"))
        {
            // BossEnergyBallと接触した場合、エネルギー弾を消去
            Destroy(gameObject);
        }
    }
}
