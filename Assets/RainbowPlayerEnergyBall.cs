using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowPlayerEnergyBall : MonoBehaviour
{
    public float speed = 10f;  // エネルギーボールの移動速度
    public float damage = 10f;  // 与えるダメージ量
    public float damageInterval = 0.5f;  // ダメージを与える間隔
    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>(); // 既にダメージを与えている敵の追跡
    public float lifetime = 30f;  // エネルギーボールの寿命

    void Start()
    {
        // 指定した時間後にエネルギーボールを自動的に消去する
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // エネルギーボールを z 軸方向に進める
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Alien"))
        {
            if (!hitEnemies.Contains(other.gameObject))
            {
                hitEnemies.Add(other.gameObject);
                StartCoroutine(DealDamageOverTimeAlien(other.gameObject));
            }
        }
        else if (other.CompareTag("Ufo"))  // 修正：UFO → Ufo
        {
            if (!hitEnemies.Contains(other.gameObject))
            {
                hitEnemies.Add(other.gameObject);
                StartCoroutine(DealDamageOverTimeUfo(other.gameObject));  // 修正：UFO → Ufo
            }
        }
        else if (other.CompareTag("Boss"))
        {
            if (!hitEnemies.Contains(other.gameObject))
            {
                hitEnemies.Add(other.gameObject);
                StartCoroutine(DealDamageOverTimeBoss(other.gameObject));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (hitEnemies.Contains(other.gameObject))
        {
            hitEnemies.Remove(other.gameObject);
        }
    }

    // Alien にダメージを与える
    IEnumerator DealDamageOverTimeAlien(GameObject alien)
    {
        while (hitEnemies.Contains(alien))
        {
            var health = alien.GetComponent<AlienHealth>();
            if (health != null)
            {
                health.TakeDamage(Mathf.RoundToInt(damage)); // damage を int に変換
            }
            yield return new WaitForSeconds(damageInterval); // damageInterval 秒待機
        }
    }

    // Ufo にダメージを与える
    IEnumerator DealDamageOverTimeUfo(GameObject ufo)
    {
        while (hitEnemies.Contains(ufo))
        {
            var health = ufo.GetComponent<UfoHealth>();
            if (health != null)
            {
                health.TakeDamage(Mathf.RoundToInt(damage)); // damage を int に変換
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }

    // Boss にダメージを与える
    IEnumerator DealDamageOverTimeBoss(GameObject boss)
    {
        while (hitEnemies.Contains(boss))
        {
            var health = boss.GetComponent<BossHealth>();
            if (health != null)
            {
                health.TakeDamage(Mathf.RoundToInt(damage)); // damage を int に変換
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
