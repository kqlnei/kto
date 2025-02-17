using UnityEngine;

public class BoxMover : MonoBehaviour
{
    public float speed = 2f;      // �ړ����x
    public float distance = 5f;  // �ړ�����
    private Vector3 startPosition;
    private float spawnTime;     // �������ꂽ�u�Ԃ̎���

    void Start()
    {
        // �����ʒu�Ɛ������Ԃ��L�^
        startPosition = transform.position;
        spawnTime = Time.time; // ���̃_���{�[�����������ꂽ�u�Ԃ̎���
    }

    void Update()
    {
        // �������ꂽ�u�Ԃ̎��Ԃ���Ɉړ��������v�Z
        float elapsedTime = Time.time - spawnTime; // ���̃_���{�[���̌o�ߎ���
        float offset = elapsedTime * speed;  // Time.deltaTime�͕s�v

        transform.position = startPosition + new Vector3(offset, 0, 0);

        // �����𒴂�����I�u�W�F�N�g���폜
        if (offset >= distance)
        {
            Destroy(gameObject);
        }
    }

}