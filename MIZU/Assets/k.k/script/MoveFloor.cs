using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class KK_MoveFloor : MonoBehaviour
{
    public enum MoveMode
    {
        Vertical, // �����Ǐ]
        Horizontal    // �W�O�U�O�Ǐ]
    }

    public MoveMode moveMode = MoveMode.Vertical; // ���݂̒Ǐ]���[�h
    public float speed = 3f;                           // �ړ����x

    public Vector3 startPoint; // ���̊J�n�ʒu
    public Vector3 endPoint;   // ���̏I���ʒu

    private bool movingToEnd = true;

    public void Start()
    {
        startPoint = transform.position;
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

    // �W�O�U�O�Ǐ]�̏���
    private void HorizontalFollow()
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
}