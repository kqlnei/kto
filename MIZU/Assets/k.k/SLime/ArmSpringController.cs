using UnityEngine;
using System.Collections;

public class ArmSpringController : MonoBehaviour
{
    public GameObject springJointObject; // SpringJoint�����I�u�W�F�N�g

    private SpringJoint springJoint;

    void Start()
    {
        // springJointObject���ݒ肳��Ă��邩�m�F
        if (springJointObject != null)
        {
            // SpringJoint�R���|�[�l���g���擾
            springJoint = springJointObject.GetComponent<SpringJoint>();
            if (springJoint == null)
            {
                Debug.LogError("�w�肳�ꂽ�I�u�W�F�N�g��SpringJoint�R���|�[�l���g��������܂���B");
            }
        }
        else
        {
            Debug.LogError("SpringJoint�����I�u�W�F�N�g���w�肳��Ă��܂���B");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Player�^�O�̂����I�u�W�F�N�g�ƐڐG������
        if (collision.gameObject.CompareTag("Player"))
        {
            if (springJoint != null)
            {
                StartCoroutine(ChangeSpringValue());
            }
        }
    }

    private IEnumerator ChangeSpringValue()
    {
        float startValue = springJoint.spring;
        float targetValue = 100f;
        float elapsedTime = 0f;
        float duration = 1.5f; // 2�b�����ĕω�

        // 2�b�����ď��X��300�ɕω�
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            springJoint.spring = Mathf.Lerp(startValue, targetValue, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
       
        Debug.Log("SpringJoint �� Spring �l�� 300 �ɐݒ肳��܂����B");

        // 2�b�ҋ@
        //yield return new WaitForSeconds(1000f);

        // 0�ɖ߂�
        springJoint.spring = 0f;
        Debug.Log("SpringJoint �� Spring �l�� 0 �ɖ߂���܂����B");
    }
}
