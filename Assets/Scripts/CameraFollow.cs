using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float smoothSpeed = 0.125f; // 카메라 이동의 부드러움
    private float cameraYThreshold; // 카메라가 움직이기 시작할 y 위치

    private void Start()
    {
        // 카메라의 중앙 위치를 기준으로 threshold 설정
        cameraYThreshold = transform.position.y;
    }

    private void LateUpdate()
    {
        // 플레이어의 위치가 카메라의 중앙 위치를 넘었을 때만 카메라 이동 시작
        if (player.position.y > cameraYThreshold)
        {
            // 목표 위치 계산 (x축은 고정, y축은 플레이어 위치로 이동)
            Vector3 desiredPosition = new Vector3(transform.position.x, player.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
