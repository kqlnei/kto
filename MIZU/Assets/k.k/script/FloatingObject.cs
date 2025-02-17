using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    // �g�̍���
    public float waveHeight = 0.5f;
    // �g�̑���
    public float waveSpeed = 1.0f;
    // �g�̒���
    public float waveLength = 1.0f;

    // �I�u�W�F�N�g�̏����ʒu
    private Vector3 startPosition;

    void Start()
    {
        // �����ʒu��ۑ�
        startPosition = transform.position;
    }

    void Update()
    {
        // �g�̓����̌v�Z
        float newY = startPosition.y + Mathf.Sin(Time.time * waveSpeed + transform.position.x / waveLength) * waveHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
