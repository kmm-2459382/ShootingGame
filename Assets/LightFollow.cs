using UnityEngine;

public class LightFollow : MonoBehaviour
{
    // プレイヤーオブジェクト
    public Transform player;

    // y軸のオフセット値
    public float yOffset = 2f;

    void Update()
    {
        // プレイヤーが設定されている場合にのみ処理
        if (player != null)
        {
            // Directional Lightの位置をプレイヤーのy座標 + yOffsetに設定
            transform.position = new Vector3(transform.position.x, player.position.y + yOffset, transform.position.z);
        }
    }
}
