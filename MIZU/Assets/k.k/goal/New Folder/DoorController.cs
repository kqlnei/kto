using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animator;

    // ������
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // �h�A���J����
    public void OpenDoor()
    {
        animator.SetBool("goal", true);
    }

    // �h�A��߂�
    public void CloseDoor()
    {
        animator.SetBool("goal", false);
    }
}