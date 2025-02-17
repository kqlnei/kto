using UnityEngine;

public class KK_PlayerCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform object1; // �Ǐ]����I�u�W�F�N�g1
    [SerializeField] private Transform object2; // �Ǐ]����I�u�W�F�N�g2
    [SerializeField] private float offset = 10f; // �J�����ƃI�u�W�F�N�g�̋���

    private void LateUpdate()
    {
        if (object1 == null || object2 == null)
            return;

        // 2�̃I�u�W�F�N�g�̒��S���v�Z
        Vector3 centerPoint = (object1.position + object2.position) / 2;

        // 2�̃I�u�W�F�N�g�Ԃ̋������v�Z
        float distance = Vector3.Distance(object1.position, object2.position);

        // �J�����̃T�C�Y���v�Z�i��ʂɎ��߂邽�߂̍ŏ��T�C�Y������j
        Camera camera = GetComponent<Camera>();
        camera.orthographicSize = Mathf.Max(distance / 2 + offset, 5f); // ��ʂɎ��߂邽�߂̃T�C�Y

        // �J�����̈ʒu��ݒ�
        transform.position = new Vector3(centerPoint.x, centerPoint.y, transform.position.z);
    }
}
