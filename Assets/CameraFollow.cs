using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // プレイヤーオブジェクト（Transformとして公開）
    public Transform player;

    // カメラがプレイヤーからどれだけ離れているかを設定するオフセット
    public Vector3 offset = new Vector3(0, 4, -8);

    // カメラの回転を設定するための角度（ピッチ、ヨー、ロール）
    public Vector3 rotationAngles = new Vector3(20, 0, 0);

    void LateUpdate()
    {
        // プレイヤーの座標 + オフセットでカメラの位置を設定
        if (player != null)
        {
            transform.position = player.position + offset;

            // カメラの回転を設定
            transform.rotation = Quaternion.Euler(rotationAngles);
        }
    }
}
