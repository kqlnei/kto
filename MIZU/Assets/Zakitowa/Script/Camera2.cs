using UnityEngine;

public class Camera2 : MonoBehaviour
{
    public Transform player;    // �v���C���[�I�u�W�F�N�g��Transform
    public Vector3 offset;      // �J�����ƃv���C���[�Ԃ̃I�t�Z�b�g

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
