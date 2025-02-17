using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    public enum MoveMode
    {
        Vertical,    // �����Ǐ]
        Horizontal   // �W�O�U�O�Ǐ]
    }

    public MoveMode moveMode = MoveMode.Vertical; // ���݂̒Ǐ]���[�h
    public float speed = 3f;                      // �ړ����x

    public Vector3 relativeEndPoint = new Vector3(0, 5, 0); // �J�n�ʒu����̑��΍��W
    private Vector3 startPoint;   // ���̊J�n�ʒu
    private Vector3 endPoint;     // ���̏I���ʒu

    private bool movingToEnd = true;

    public void Start()
    {
        // ���ݒn���J�n�ʒu�Ƃ��Đݒ�
        startPoint = transform.position;

        // �I���ʒu�𑊑ΓI�Ɍv�Z
        endPoint = startPoint + relativeEndPoint;
    }

    public void Update()
    {
        // ���[�h�ɉ����ď�����؂�ւ�
        switch (moveMode)
        {
            case MoveMode.Vertical:
                VerticalFollow();
                break;

            case MoveMode.Horizontal:
                HorizontalFollow();
                break;
        }
    }

    // �����Ǐ]�̏���
    private void VerticalFollow()
    {
        // �����ړ�
        if (movingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, endPoint) < 0.1f)
            {
                movingToEnd = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, startPoint) < 0.1f)
            {
                movingToEnd = true;
            }
        }
    }

    private void HorizontalFollow()
    {
        // ���������̒Ǐ]�����������Ɏ���
        // �K�v�ɉ����ăp�^�[����ύX
    }
}
