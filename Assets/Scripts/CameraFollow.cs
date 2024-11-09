using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public float smoothSpeed = 0.125f; // ī�޶� �̵��� �ε巯��
    private float cameraYThreshold; // ī�޶� �����̱� ������ y ��ġ

    private void Start()
    {
        // ī�޶��� �߾� ��ġ�� �������� threshold ����
        cameraYThreshold = transform.position.y;
    }

    private void LateUpdate()
    {
        // �÷��̾��� ��ġ�� ī�޶��� �߾� ��ġ�� �Ѿ��� ���� ī�޶� �̵� ����
        if (player.position.y > cameraYThreshold)
        {
            // ��ǥ ��ġ ��� (x���� ����, y���� �÷��̾� ��ġ�� �̵�)
            Vector3 desiredPosition = new Vector3(transform.position.x, player.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
