using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class changeRiseGasTag : MonoBehaviour
{
    [Header("��]��Ԃɉ����ă^�O��t�^�E��������Ώۂ̃I�u�W�F�N�g")]
    [SerializeField] private GameObject riseGusObject;
    [Header("�t�^�������^�O�̖��O����͂���")]
    [Tooltip("(�㏸:riseGas)(�E�Ɉړ�:moveRightGas)")]
    [SerializeField] private string activeTag;

    private RotateObject rotateObject;


    void Awake()
    {
        rotateObject = GetComponent<RotateObject>();

        if (rotateObject == null)
        {
            Debug.LogError($"RotateObject��{gameObject.name}�Ɍ�����܂���B");
            return;
        }

        if (riseGusObject == null)
        {
            Debug.LogError($"RiseGasObject��{gameObject.name}�ɐݒ肳��Ă��܂���B");
            return;
        }
        if (string.IsNullOrEmpty(activeTag))
        {
            Debug.LogError($"ActiveTag��{gameObject.name}�ɐݒ肳��Ă��܂���B");
            return;
        }

        //  RotateObject�̃C�x���g��ǂ�
        rotateObject.RotatingChange += RotateStateChange;

        //  ������ԂŃ^�O��ݒ肷��
        UpdateTag(rotateObject.IsRotating);
    }

    //  �X�N���v�g���j������鎞�ɁAAwake()�̃C�x���g��ǂލs�ׂ�j������
    void OnDestroy()
    {
        if (rotateObject != null)
        {
            rotateObject.RotatingChange -= RotateStateChange;
        }
    }

    //  RotateObject�̉�]��Ԃ��ω������ۂɌĂяo����郁�\�b�h
    private void RotateStateChange(RotateObject rotate, bool isRotating)
    {
        UpdateTag(isRotating);
    }

    private void UpdateTag(bool isRotating)
    {
        if(isRotating) 
        {
            //  ���ł�"riseGas"�̃^�O���t���Ă���ꍇ�A�������Ȃ�
            if (riseGusObject.CompareTag(activeTag)) return;

            //  "riseGas"�̃^�O�̕t�^
            riseGusObject.tag = activeTag;
            Debug.Log($"{riseGusObject.name}��'{activeTag}'�̃^�O��t�^�����B");
            return;
        }

        //  "riseGas"�̃^�O���t���Ă��Ȃ��ꍇ�A�������Ȃ�
        if (!riseGusObject.CompareTag(activeTag)) return;

        //  "Untagged"�̃^�O�ɕύX����
        riseGusObject.tag = "Untagged";
        Debug.Log($"{riseGusObject.name}����'{activeTag}'�̃^�O�������܂����B");
    }
}
