using Cysharp.Threading.Tasks.Triggers;
using Unity.VisualScripting;
using UnityEngine;

public class WaterControl : MonoBehaviour
{
    public GameObject water; // �㉺���鐅�I�u�W�F�N�g
    public float ascendAmount = 1.0f; // �㏸���̈ړ���
    public float descendAmount = 1.0f; // ���~���̈ړ���
    public float moveSpeed = 2.0f; // �ړ����x
    public bool isAscending = true; // �㏸���邩���~���邩(True�Ȃ�オ��)

    private bool isMoving = false; // ���݈ړ������ǂ���
    private Vector3 targetPosition; // ���̖ڕW�ʒu
    private Vector3 initialPosition;//�����ʒu
    [SerializeField]
    private MM_PlayerSpownTest _spowntest;
    MM_ObserverBool _observer;

    public GameObject buttonTop;  // ������镔���̃I�u�W�F�N�g
    private Material buttonMaterial; // �{�^���㕔�̃}�e���A��
    public Material pressedMaterial;  // �����ꂽ�Ƃ��̐F
    public float pressOffset = 0.01f;   // �{�^���������鋗��

    private Vector3 initialbuttonPosition;  // �{�^���㕔�̏����ʒu

    private void Start()
    {
        _observer = new MM_ObserverBool();
        initialPosition = water.transform.position;

        initialbuttonPosition = buttonTop.transform.localPosition;
        buttonMaterial = buttonTop.GetComponent<Renderer>().material;
    }
    void Update()
    {
        if (isMoving)
        {
            // ���݂̈ʒu��ڕW�ʒu�Ɍ����Ĉړ�
            water.transform.position = Vector3.MoveTowards(
                water.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            // �ڕW�ʒu�ɓ��B������ړ����~
            if (Vector3.Distance(water.transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }

        if (_observer.OnBoolTrueChange(_spowntest.GetIsRespown()))
        {
            ResetWater();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving) // �ړ����łȂ��ꍇ�̂ݓ���
        {

            //  �v���C���[�̏�Ԃ��擾����
            var playerPhaseState = other.gameObject.GetComponent<MM_PlayerPhaseState>();

            //  �v���C���[��Solid�̏�Ԃł͂Ȃ��ꍇ�ɕԂ�
            if (playerPhaseState == null || playerPhaseState.GetState() != MM_PlayerPhaseState.State.Solid)
            {
                Debug.Log($"{gameObject.name}: �v���C���[��Solid�̏�Ԃł͂Ȃ�");
                return;
            }

            // �ړ��ڕW�ʒu��ݒ�
            float direction = isAscending ? ascendAmount : -descendAmount;
            targetPosition = water.transform.position + new Vector3(0, direction, 0);

            // �ړ����J�n
            isMoving = true;

            // �R���C�_�[���ꎞ�I�ɖ�����
            this.GetComponent<Collider>().enabled = false;

            // �{�^���㕔��������
            buttonTop.transform.localPosition = new Vector3(
                initialbuttonPosition.x,
                initialbuttonPosition.y - pressOffset,
                initialbuttonPosition.z
            );

            // �F��ύX
            buttonTop.GetComponent<Renderer>().material = pressedMaterial;

            MM_SoundManager.Instance.PlaySE(MM_SoundManager.SoundType.ButtonPush);
            MM_SoundManager.Instance.PlaySE(MM_SoundManager.SoundType.WaterUpDown);

        }

    }

    public void ResetWater()
    {
        water.transform.position = initialPosition;
        isMoving = false;
        this.GetComponent<Collider>().enabled = true; // �R���C�_�[���ēx�L����


        buttonTop.transform.localPosition = initialbuttonPosition;
        // �F��������Ԃɖ߂�
        buttonTop.GetComponent<Renderer>().material = buttonMaterial;

    }
}