using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;

public class BackImageButton : MonoBehaviour
{
    private PlayerInputActions backInputActions;  //  �����������ꂽ���̓A�N�V�����N���X�̃C���X�^���X

    [Header("ImageSwitcher�N���X�̎Q��")]
    [SerializeField] private ImageSwitcher imageSwitcher;

    void Awake()
    {
        backInputActions = new PlayerInputActions();  //  PlayerInputActions�C���X�^���X�̏�����
    }

    void OnEnable()
    {
        backInputActions.GamePlay.Back.performed += OnBackButton;  //  �C�x���g���X�i�[��o�^����
        backInputActions.GamePlay.Back.Enable();  //  �A�N�V������L��������
    }

    void OnDisable()
    {
        backInputActions.GamePlay.Back.performed -= OnBackButton;  //  �C�x���g���X�i�[����������
        backInputActions.GamePlay.Back.Disable();  //  �A�N�V�����𖳌�������
    }

    private void OnBackButton(InputAction.CallbackContext context)
    {
        //  ImageSwitcher���ݒ肳��Ă����ꍇ�AHandleBackImage��Ԃ�
        if (imageSwitcher != null)
        {
            imageSwitcher.HandleBackImage();
        }
    }
}
