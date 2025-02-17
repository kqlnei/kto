using UnityEngine;

public class T_YPlayer : MonoBehaviour
{
    public float speed = 5.0f; // �v���C���[�̈ړ����x

    void Update()
    {
        // ���������̓��́iA��D�j
        float horizontal = Input.GetAxis("Horizontal");
        // ���������̓��́iW��S�j
        float vertical = Input.GetAxis("Vertical");

        // ���͂Ɋ�Â��Ĉړ��������v�Z
        Vector2 direction = new Vector2(horizontal, vertical).normalized;

        // �ړ�����
        if (direction.magnitude >= 0.1f)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
