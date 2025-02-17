using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinManager : MonoBehaviour
{

    // �v���C���[���Q�[����Join���邽�߂�InputAction
    [SerializeField] private InputAction playerJoinInputAction;

    [SerializeField] private InputActionAsset inputasset;
    // PlayerInput���A�^�b�`����Ă���v���C���[�I�u�W�F�N�g
    [SerializeField] private PlayerInput playerPrefab = default;

    // �ő�Q���l��
    [SerializeField] private int maxPlayerCount = default;


    // Join�ς݂̃f�o�C�X���
    private InputDevice[] joinedDevices = default;
    // ���݂̃v���C���[��
    private int currentPlayerCount = 0;


    private void Awake()
    {
        playerJoinInputAction = inputasset.actionMaps[0].FindAction("Move");
        // �ő�Q���\���Ŕz���������
        joinedDevices = new InputDevice[maxPlayerCount];
        // InputAction��L�������A�R�[���o�b�N��ݒ�
        playerJoinInputAction.Enable();
        playerJoinInputAction.performed += OnJoin;
    }

    private void OnDestroy()
    {
        playerJoinInputAction.Dispose();
    }

    /// <summary>
    /// �f�o�C�X�ɂ����Join�v�������΂����Ƃ��ɌĂ΂�鏈��
    /// </summary>
    private void OnJoin(InputAction.CallbackContext context)
    {
        // �v���C���[�����ő吔�ɒB���Ă�����A�������I��
        if (currentPlayerCount >= maxPlayerCount)
        {
            return;
        }

        // Join�v�����̃f�o�C�X�����ɎQ���ς݂̂Ƃ��A�������I��
        foreach (var device in joinedDevices)
        {
            if (context.control.device == device)
            {
                return;
            }
        }

        // PlayerInput�������������z�̃v���C���[���C���X�^���X��
        // ��Join�v�����̃f�o�C�X����R�Â��ăC���X�^���X�𐶐�����
        PlayerInput.Instantiate(
        prefab: playerPrefab.gameObject,
        playerIndex: currentPlayerCount,
        pairWithDevice: context.control.device
        );

        // Join�����f�o�C�X����ۑ�
        joinedDevices[currentPlayerCount] = context.control.device;

        currentPlayerCount++;
    }
}
