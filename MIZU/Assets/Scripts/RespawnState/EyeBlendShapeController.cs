using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlendShapeController : MonoBehaviour
{
    [Header("TriggerAnimationMask�ւ̎Q��")]
    [SerializeField] private TriggerAnimationMask mask;

    [Header("�ڂ��J����Ώۂ̎q�I�u�W�F�N�g")]
    [SerializeField] private GameObject player1EyesObject;
    [SerializeField] private GameObject player2EyesObject;

    [Header("�ڂ��J�����߂̃u�����h�V�F�C�v�̃C���f�b�N�X")]
    [SerializeField] private int player1EyesOpenBlendShapeIndex = 0;
    [SerializeField] private int player2EyesOpenBlendShapeIndex = 0;

    [Header("�ڂ��J����ۂ̃u�����h�V�F�C�v�̏d��")]
    [SerializeField]private float openWeight = 10f;

    private Animator animator;
    private bool hasAnimationPlayed = false;

    void Start()
    {
        //  �K�v�ȎQ�Ƃ��ݒ肳��Ă��邩�m�F
        if (mask == null)
        {
            Debug.LogError("mask���A�^�b�`����Ă��Ȃ�");
            return;
        }

        if (!mask.TryGetComponent<Animator>(out animator))
        {
            Debug.LogError("mask��Animator�R���|�[�l���g�����݂��Ă��Ȃ�");
        }

        if (player1EyesObject == null || player2EyesObject == null)
        {
            Debug.LogError("player1EyesObject��player2EyesObject���A�^�b�`����Ă��Ȃ�");
        }
    }

    //  AnimationEvent����Ăяo����郁�\�b�h
    public void OnAnimationComplete()
    {
        if (!hasAnimationPlayed)
        {
            hasAnimationPlayed = true;
            OpenEyes();
        }
    }

    //  �ڂ��J����u�����h�V�F�C�v�����肷�郁�\�b�h
    private void OpenEyes()
    {
        //  player1�̉��ʂƓ����`�̂��̂�SkinnedMeshRenderer���擾����
        if (player1EyesObject.TryGetComponent<SkinnedMeshRenderer>(out var player1EyeRenderer))
        {
            player1EyeRenderer.SetBlendShapeWeight(player1EyesOpenBlendShapeIndex, openWeight);
            Debug.Log("�����̉��ʂ̖ڂ��J����");
        }

        //  player2�̉��ʂƓ����`�̂��̂�SkinnedMeshRenderer���擾����
        if (player2EyesObject.TryGetComponent<SkinnedMeshRenderer>(out var player2EyeRenderer))
        {
            player2EyeRenderer.SetBlendShapeWeight(player2EyesOpenBlendShapeIndex, openWeight);
            Debug.Log("�E���̉��ʂ̖ڂ��J����");
        }

    }
}
