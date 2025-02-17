using UnityEngine;
using System.Collections;

public class KK_PlayerModelSwitcher : MonoBehaviour
{
    public GameObject liquidModel;   // �t�̏�Ԃ̃��f��
    public GameObject gasModel;      // �C�̏�Ԃ̃��f��
    public GameObject solidModel;    // �ő̏�Ԃ̃��f��
    public GameObject slimeModel;    // �X���C����Ԃ̃��f��

    public GameObject transformationEffect;  // �ϐg�G�t�F�N�g�p�̃I�u�W�F�N�g
    public float effectDuration = 2f;        // �G�t�F�N�g�̕\�����ԁi�b�j

    [HideInInspector] public GameObject currentModel; // ���ݕ\�����Ă��郂�f��

    void Start()
    {
        // ������Ԃ��t�̃��f���ɐݒ�
        SwitchToModel(liquidModel);

        // �ϐg�G�t�F�N�g���Q�[���J�n���ɔ�A�N�e�B�u�ɐݒ�
        if (transformationEffect != null)
        {
            transformationEffect.SetActive(false);  // �Q�[���J�n���ɃG�t�F�N�g�𖳌���
            //Debug.Log("Transformation effect set to inactive at game start.");
        }
    }

    public void SwitchToModel(GameObject newModel)
    {
        ///Debug.Log("Switching model to: " + newModel.name);

        // ���݂̃��f��������Δ�A�N�e�B�u�ɂ���
        if (currentModel != null)
        {
            currentModel.SetActive(false);
            Debug.Log("Previous model deactivated: " + currentModel.name);
        }

        // �V�������f����ݒ肵�ăA�N�e�B�u�ɂ���
        currentModel = newModel;
        currentModel.SetActive(true);
        //Debug.Log("New model activated: " + currentModel.name);

        // �ϐg�G�t�F�N�g��\�����鏈�����J�n
        if (transformationEffect != null)
        {
            StartCoroutine(PlayTransformationEffect());
        }
    }

    // �ϐg�G�t�F�N�g�𐔕b�ԕ\������R���[�`��
    private IEnumerator PlayTransformationEffect()
    {
        // �G�t�F�N�g���A�N�e�B�u�ɂ���
        transformationEffect.SetActive(true);
       // Debug.Log("Transformation effect activated.");

        // �ݒ肵���b�������҂�
        yield return new WaitForSeconds(effectDuration);

        // �G�t�F�N�g���A�N�e�B�u�ɂ���
        transformationEffect.SetActive(false);
       // Debug.Log("Transformation effect deactivated.");
    }
}
