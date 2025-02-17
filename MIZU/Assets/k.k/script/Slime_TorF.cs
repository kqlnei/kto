using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_TorF : MonoBehaviour
{
    public GameObject childObject; // ��Ԃ��Ď�����q�I�u�W�F�N�g
    public SlimeCamera slimeCamera; // �X�N���v�g2���Q��

    private bool lastState; // �Ō�ɋL�^���ꂽ���

    void Start()
    {
        if (childObject != null)
        {
            lastState = childObject.activeSelf; // ������Ԃ��L�^
        }
    }

    void Update()
    {
        if (childObject != null)
        {
            bool currentState = childObject.activeSelf;

            // ��Ԃ��ς�����ꍇ�̂ݒʒm
            if (currentState != lastState)
            {
                lastState = currentState;

                // �X�N���v�g2�ɒʒm
                if (slimeCamera != null)
                {
                    slimeCamera.UpdateChildState(currentState);
                }
            }
        }
    }
}
