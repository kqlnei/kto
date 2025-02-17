using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArea : MonoBehaviour
{
    [SerializeField] private float healAmount = 100f; // �񕜗�
    [SerializeField] private float healInterval = 1f; // �񕜊Ԋu

    private List<GaugeController> playersInArea = new List<GaugeController>(); // �G���A���̃v���C���[���X�g

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("bbbbbbbbbbbbbbbbbbbbb");
            GaugeController playerGauge = other.GetComponent<GaugeController>();
            if (playerGauge != null && !playersInArea.Contains(playerGauge))
            {
                playersInArea.Add(playerGauge); // �G���A���̃v���C���[��ǉ�
                StartCoroutine(HealPlayer(playerGauge)); // �񕜂��J�n
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GaugeController playerGauge = other.GetComponent<GaugeController>();
            if (playerGauge != null && playersInArea.Contains(playerGauge))
            {
                playersInArea.Remove(playerGauge); // �G���A���̃v���C���[���폜
            }
        }
    }

    private IEnumerator HealPlayer(GaugeController playerGauge)
    {
        while (playersInArea.Contains(playerGauge))
        {
            playerGauge.Heal(healAmount); // �v���C���[����
            yield return new WaitForSeconds(healInterval); // �w�莞�Ԃ��Ƃɉ�
        }
    }
}
