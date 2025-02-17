using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject block; // �傫���������u���b�N�I�u�W�F�N�g�������ɃA�T�C�����܂�
    private bool isPressed = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPressed)
        {
            isPressed = true;
            IncreaseBlockSize();
        }
    }

    void IncreaseBlockSize()
    {
        if (block != null)
        {
            block.transform.localScale *= 2; // �u���b�N�̃T�C�Y��2�{�ɂ��܂�
        }
    }
}
