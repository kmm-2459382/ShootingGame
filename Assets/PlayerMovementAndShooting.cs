using UnityEngine;

public class PlayerMovementAndShooting : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -8f;
    public float maxZ = 11f;
    public GameObject energyBallPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    private float fireRate = 0.5f;
    private float nextFireTime = 0f;

    // Joystick 変数を追加
    [SerializeField] private FloatingJoystick floatingJoystick;

    void Start()
    {
        // Joystick がインスペクターで設定されていない場合、シーン内から見つける
        if (floatingJoystick == null)
        {
            floatingJoystick = FindObjectOfType<FloatingJoystick>();
        }
    }

    void Update()
    {
        // プレイヤーの移動処理
        MovePlayer();

        // 発射タイミングの管理
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void MovePlayer()
    {
        float moveX = 0f;
        float moveY = 0f;

        // キーボード操作の入力を取得
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            moveX = Input.GetAxis("Horizontal");
            moveY = Input.GetAxis("Vertical");
        }
        // Joystick操作の入力を取得
        else if (floatingJoystick != null)
        {
            moveX = floatingJoystick.Horizontal;
            moveY = floatingJoystick.Vertical;
        }

        // 移動ベクトルの計算
        Vector3 movement = new Vector3(moveX, 0f, moveY);
        Vector3 newPosition = transform.position + movement * moveSpeed * Time.deltaTime;

        // プレイヤーの位置をクランプして範囲内に制限
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        // 新しい位置を設定
        transform.position = newPosition;
    }

    void Shoot()
    {
        // エネルギーボールの生成
        GameObject bullet = Instantiate(energyBallPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;

        // 一定時間後にエネルギーボールを破壊
        StartCoroutine(DestroyBulletAfterTime(bullet, 5.0f));
    }

    private System.Collections.IEnumerator DestroyBulletAfterTime(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        if (bullet != null)
        {
            Destroy(bullet);
        }
    }
}
