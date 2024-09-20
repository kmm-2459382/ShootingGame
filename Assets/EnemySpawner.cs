using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject alienPrefab;   // AlienのPrefab
    public GameObject ufoPrefab;     // UFOのPrefab
    public GameObject bossPrefab;    // BossのPrefab

    private GameObject currentBoss;  // フィールド上に存在するボス
    public float minSpawnTime = 2f;  // 最短スポーン時間 (UFO, Alien用)
    public float maxSpawnTime = 7f;  // 最長スポーン時間 (UFO, Alien用)
    public float bossMinRespawnTime = 10f;  // ボスのリスポーン最短時間
    public float bossMaxRespawnTime = 30f;  // ボスのリスポーン最長時間
    public float enemyLifeTime = 10f;     // UFOとAlienのデスポーン時間

    void Start()
    {
        // UFOとAlienのスポーンを開始
        StartCoroutine(SpawnAlienAndUFO());

        // 最初のボスのスポーンを開始（ゲーム開始後、10〜30秒後）
        float initialBossSpawnDelay = Random.Range(bossMinRespawnTime, bossMaxRespawnTime);
        StartCoroutine(SpawnBossWithDelay(initialBossSpawnDelay));
    }

    // UFOとAlienをランダムな間隔でスポーンさせる
    IEnumerator SpawnAlienAndUFO()
    {
        while (true)
        {
            // ランダムな時間待機
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            // Alienのスポーン位置 (x座標ランダム)
            Vector3 alienSpawnPos = new Vector3(Random.Range(-10f, 10f), -2f, 20f);
            GameObject alien = Instantiate(alienPrefab, alienSpawnPos, Quaternion.Euler(0, 180, 0));  // Alienは180度回転させる

            // UFOのスポーン位置 (x座標ランダム)
            Vector3 ufoSpawnPos = new Vector3(Random.Range(-10f, 10f), 0f, 20f);
            GameObject ufo = Instantiate(ufoPrefab, ufoSpawnPos, Quaternion.Euler(0, 180, 0));  // UFOは180度回転させる

            // 10秒後にAlienとUFOをデスポーン（削除）
            Destroy(alien, enemyLifeTime);
            Destroy(ufo, enemyLifeTime);
        }
    }

    // Bossのスポーンを行うメソッド
    IEnumerator SpawnBossWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // 指定した遅延時間を待機

        // フィールド上にボスがいない場合のみスポーン
        if (currentBoss == null)
        {
            Vector3 bossSpawnPos = new Vector3(0f, 1f, 24f);  // ボスのスポーン座標 (固定)
            currentBoss = Instantiate(bossPrefab, bossSpawnPos, Quaternion.Euler(0, 180, 0));  // ボスを180度回転させてスポーン

            // BossHealthコンポーネントがあれば、ボスの死亡時にリスポーン処理を開始
            BossHealth bossHealth = currentBoss.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.OnBossDeath += HandleBossDeath;  // ボスの死亡イベントを監視
            }
        }
    }

    // ボスが倒されたときの処理
    void HandleBossDeath()
    {
        currentBoss = null;  // 現在のボスをクリア

        // ボスが倒された後、10〜30秒の間でランダムな遅延をもってリスポーン
        float respawnDelay = Random.Range(bossMinRespawnTime, bossMaxRespawnTime);
        StartCoroutine(SpawnBossWithDelay(respawnDelay));
    }
}
