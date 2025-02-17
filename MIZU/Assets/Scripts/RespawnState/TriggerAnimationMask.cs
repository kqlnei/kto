using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimationMask : MonoBehaviour
{
    [Header("MM_PlayerTrigger�R���|�[�l���g�ւ̎Q��")]
    [SerializeField] private MM_PlayerTrigger playerTrigger;

    [Header("�A�j���[�V�������Đ�����A�j���[�^�[")]
    [SerializeField] private Animator animator;

    [Header("�A�j���[�^�[�̃g���K�[��")]
    [SerializeField] private string triggerName = "Move Trigger";

    private bool animationTrigger = false;  //  �A�j���[�V�������Đ����邽�߂̃t���O

    void Update()
    {
        if(playerTrigger.GetIsTrigger() && !animationTrigger)
        {
            PlayAnimation();
            animationTrigger = true;
        }
    }

    //  �A�j���[�V�������Đ����郁�\�b�h
    private void PlayAnimation()
    {
        if(animator != null)
        { 
            animator.SetTrigger(triggerName);
            Debug.Log("�A�j���[�V�������Đ�����");
        }
        else
        {
            Debug.LogWarning("�A�j���[�^�[���A�T�C������Ă��Ȃ�");
        }
    }
}
