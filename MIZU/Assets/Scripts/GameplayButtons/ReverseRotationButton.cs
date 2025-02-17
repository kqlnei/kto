using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseRotationButton : RepressableButton
{
    [Header("�Q�Ƃ���RotateObject")]
    [SerializeField] private RotateObject rotateObject;

    /// 
    [Header("�Ώۂ̃I�u�W�F�N�g")]
    [SerializeField] private GameObject targetObject;
    /// 

    public override void Execute()
    {
        if (rotateObject == null)
        {
            Debug.LogError($"{gameObject.name}: RotateObject ���ݒ肳��Ă��܂���B");
            return;
        }

        //  ���݂̉�]�����𔽓]����
        int newDirection = -rotateObject.RotateDirection;
        rotateObject.SetRotationDirection(newDirection);

        Debug.Log($"{gameObject.name}: RotateObject�̉�]������{(newDirection == 1 ? "���]" : "�t��]")}�ɕύX�����B");


        ToggleRotation();
    }



    /// 
    public void ToggleRotation()
    {
        Debug.Log("hhhhhhhhhhhhhhhhhhhhhh");
        if (targetObject == null)
        {
            Debug.LogWarning("�^�[�Q�b�g�I�u�W�F�N�g���ݒ肳��Ă��Ȃ�");
            return;
        }

        // ���݂̉�]�p���擾
        Vector3 currentRotation = targetObject.transform.eulerAngles;

        // x���̉�]��180�x���]
        currentRotation.x = (currentRotation.x + 180) % 360;

        // �V������]�p��K�p
        targetObject.transform.eulerAngles = currentRotation;
    }
    ///
}
