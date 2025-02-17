using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MM_PlayerPhaseState))]
[RequireComponent(typeof(MM_GroundCheck))]

public class Alpha_Player : MonoBehaviour
{
    [SerializeField]
    private bool isPlayerOneMode = true; // True: Player 1 mode (WAD1234), False: Player 2 mode (IJL7890)


    //[SerializeField]
    //[Header("�f�o�b�O���[�h")]
    //bool IS_DEBUGMODE = false;
    [SerializeField]
    private float _defaultGravity;
    [SerializeField]
    private float nowGravity;
    [SerializeField]
    private float _JumpPower;
    [SerializeField]
    private float _MovePower;
    [SerializeField]
    private float _LimitSpeed;
    [SerializeField, Header("������,-1~10")]
    private float _InertiaPower;
    [SerializeField]
    private float _NowXSpeed;
    [SerializeField]
    private float _NowYSpeed;
    [SerializeField]
    private Material[] _playerMaterials = new Material[2];

    bool isOnGround = false;
    bool isOnWater = false;

    Rigidbody _rb;
    MeshRenderer _meshRenderer;
    MM_PlayerPhaseState _pState;
    MM_GroundCheck _groundCheck;

    [SerializeField]
    TextMeshProUGUI Debug_Phasetext;

    private Vector3 _velocity;

    private KK_PlayerModelSwitcher _modelSwitcher;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _pState = GetComponent<MM_PlayerPhaseState>();
        _groundCheck = GetComponent<MM_GroundCheck>();
        _modelSwitcher = GetComponent<KK_PlayerModelSwitcher>(); // PlayerModelSwitcher �R���|�[�l���g���擾

        // Assume player index based on an external condition for materials
        _meshRenderer.material = _playerMaterials[0]; // Default to first material

        _pState.ChangeState(MM_PlayerPhaseState.State.Liquid);

        nowGravity = _defaultGravity;

        _InertiaPower = Mathf.Clamp(_InertiaPower, -1, 10);
    }

    private void Update()
    {
        if (Debug_Phasetext != null)
            Debug_Phasetext.text = "Player:" + _pState.GetState();
        PlayerStateUpdateFunc();

        HandleInput(); // Handle keyboard input in Update
    }

    private void FixedUpdate()
    {
        Gravity();
        GroundCheck();
        Move();

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Death();
        }
    }

    void Gravity()
    {
        _rb.AddForce(new Vector3(0, -nowGravity, 0), ForceMode.Acceleration);
    }

    void Move()
    {
        var nowXSpeed = Mathf.Sqrt(Mathf.Pow(_rb.velocity.x, 2));
        var nowYSpeed = Mathf.Sqrt(Mathf.Pow(_rb.velocity.y, 2));
        _NowXSpeed = nowXSpeed;
        _NowYSpeed = nowYSpeed;

        // ���ړ�
        if (_pState.GetState() != MM_PlayerPhaseState.State.Gas)
        {
            if (_velocity.x != 0)
                _rb.AddForce(_velocity, ForceMode.Acceleration);
            else
                _rb.AddForce(new Vector3(-_rb.velocity.x * _InertiaPower, _rb.velocity.y, _rb.velocity.z), ForceMode.Acceleration);
        }
        else // �C�̂̎��̏c�ړ�
        {
            if (_velocity.y != 0)
                _rb.AddForce(_velocity, ForceMode.Acceleration);
            else
                _rb.AddForce(new Vector3(_rb.velocity.x, -_rb.velocity.y * _InertiaPower, _rb.velocity.z), ForceMode.Acceleration);
        }

        if (nowXSpeed > _LimitSpeed)
        {
            _rb.velocity = new Vector3(_rb.velocity.x / (nowXSpeed / _LimitSpeed), _rb.velocity.y, _rb.velocity.z);
        }
        if (nowYSpeed > _LimitSpeed)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y / (nowYSpeed / _LimitSpeed), _rb.velocity.z);
        }
    }

    private void GroundCheck()
    {
        isOnGround = _groundCheck.IsGround();
        isOnWater = _groundCheck.IsPuddle();
    }

    private void PlayerStateUpdateFunc()
    {
        switch (_pState.GetState())
        {
            case MM_PlayerPhaseState.State.Gas: PlayerGasStateUpdateFunc(); break;
            case MM_PlayerPhaseState.State.Solid: PlayerSolidStateUpdateFunc(); break;
            case MM_PlayerPhaseState.State.Liquid: PlayerLiquidStateUpdateFunc(); break;
            default: Debug.LogError($"�G���[�A�v���C���[�̃X�e�[�g��{_pState.GetState()}�ɂȂ��Ă��܂�"); break;
        }
    }

    private void PlayerGasStateUpdateFunc() { }

    private void PlayerSolidStateUpdateFunc() { }

    private void PlayerLiquidStateUpdateFunc()
    {
        StartCoroutine(IsPuddleCollisionDeadCount());
    }

    private void PlayerSlimeStateUpdateFunc() { }

    private void Death()
    {
        this.gameObject.SetActive(false);
    }

    void HandleInput()
    {
        float moveX = 0;

        if (isPlayerOneMode) // Player 1�̑���
        {
            if (Input.GetKey(KeyCode.A)) moveX = -1;
            if (Input.GetKey(KeyCode.D)) moveX = 1;

            if (_pState.GetState() != MM_PlayerPhaseState.State.Gas)
            {
                _velocity = new Vector3(moveX * _MovePower, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.W) && isOnGround && !isOnWater && _pState.GetState() != MM_PlayerPhaseState.State.Gas)
            {
                _velocity = new Vector3(_velocity.x, 0, 0);
                _rb.AddForce(new Vector3(0, _JumpPower, 0), ForceMode.VelocityChange);
                print("Player 1 Jump");
            }

            if (_pState.GetState() == MM_PlayerPhaseState.State.Gas)
            {
                float moveY = Input.GetAxis("Vertical"); // W/S or Up/Down arrow keys
                _velocity = new Vector3(0, moveY * _MovePower, 0);
            }

            // ��ԕύX�L�[
            if (Input.GetKeyDown(KeyCode.Alpha1)) OnStateChangeGas();
            if (Input.GetKeyDown(KeyCode.Alpha2)) OnStateChangeSolid();
            if (Input.GetKeyDown(KeyCode.Alpha3)) OnStateChangeLiquid();
        }
        else // Player 2�̑���
        {
            if (Input.GetKey(KeyCode.J)) moveX = -1;
            if (Input.GetKey(KeyCode.L)) moveX = 1;

            if (_pState.GetState() != MM_PlayerPhaseState.State.Gas)
            {
                _velocity = new Vector3(moveX * _MovePower, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.I) && isOnGround && !isOnWater && _pState.GetState() != MM_PlayerPhaseState.State.Gas)
            {
                _velocity = new Vector3(_velocity.x, 0, 0);
                _rb.AddForce(new Vector3(0, _JumpPower, 0), ForceMode.VelocityChange);
                print("Player 2 Jump");
            }

            if (_pState.GetState() == MM_PlayerPhaseState.State.Gas)
            {
                float moveY = Input.GetAxis("Vertical"); // W/S or Up/Down arrow keys
                _velocity = new Vector3(0, moveY * _MovePower, 0);
            }

            // ��ԕύX�L�[
            if (Input.GetKeyDown(KeyCode.Alpha7)) OnStateChangeGas();
            if (Input.GetKeyDown(KeyCode.Alpha8)) OnStateChangeSolid();
            if (Input.GetKeyDown(KeyCode.Alpha9)) OnStateChangeLiquid();
        }

        // ���[�h�ؑւ̃L�[����
        if (Input.GetKeyDown(KeyCode.Tab)) // Tab�L�[�Ń��[�h��؂�ւ�
        {
            isPlayerOneMode = !isPlayerOneMode; // ���[�h��؂�ւ�
            Debug.Log("���[�h���؂�ւ��܂���: " + (isPlayerOneMode ? "Player 1" : "Player 2"));
        }
    }



    private IEnumerator IsPuddleCollisionDeadCount()
    {
        float contactTime = 0f;
        float destroyTime = 2f;

        while (isOnWater)
        {
            contactTime += Time.deltaTime;
            yield return null;
            if (contactTime >= destroyTime)
            {
                //Death();
            }
        }
    }

    // �ȉ��AInputSystem����ύX������ԕω����\�b�h

    public void OnStateChangeGas()
    {
        Debug.Log("Gas state change called");
        _pState.ChangeState(MM_PlayerPhaseState.State.Gas);
        nowGravity = 0;
        _rb.drag = 10;

        // ���f�����C�̂̂�ɕς��鏈��
        _modelSwitcher.SwitchToModel(_modelSwitcher.gasModel);
    }

    public void OnStateChangeSolid()
    {
        Debug.Log("Solid state change called");

        // �܂���Ԃ�ύX
        _pState.ChangeState(MM_PlayerPhaseState.State.Solid);

        // �ő̏�Ԃ̏�����
        _velocity = Vector3.zero;

        // ���f�����ő̂ɐ؂�ւ��鏈��
        Debug.Log("Switching to solid model...");
        _modelSwitcher.SwitchToModel(_modelSwitcher.solidModel);
    }

    public void OnStateChangeLiquid()
    {
        Debug.Log("Liquid state change called");
        _pState.ChangeState(MM_PlayerPhaseState.State.Liquid);
        nowGravity = _defaultGravity;
        _rb.drag = 0;

        // ���f�����t�̂̂�ɕς��鏈��
        _modelSwitcher.SwitchToModel(_modelSwitcher.liquidModel);
    }

}