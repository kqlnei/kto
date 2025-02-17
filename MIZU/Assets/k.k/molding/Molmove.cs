using UnityEngine;
using UnityEngine.InputSystem;

public class Molrmove : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveInput;

    // InputAction��Move�A�N�V���������s���ꂽ�Ƃ��̃R�[���o�b�N
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f) * speed * Time.deltaTime;

        transform.Translate(movement);
    }
}
