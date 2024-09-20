using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    // メインカメラとサブカメラの参照
    public Camera mainCamera;
    public Camera subCamera;

    void Start()
    {
        // ゲーム開始時にサブカメラを有効にし、メインカメラを無効にする
        subCamera.enabled = true;
        mainCamera.enabled = false;
    }

    void Update()
    {
        // Cキーが押されたらカメラを切り替える
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        // カメラを切り替える（サブカメラが有効ならメインカメラを有効にし、逆も同様）
        mainCamera.enabled = !mainCamera.enabled;
        subCamera.enabled = !subCamera.enabled;
    }
}
