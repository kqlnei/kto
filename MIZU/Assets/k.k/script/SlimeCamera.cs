using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCamera : MonoBehaviour
{
    public GameObject[] childObjects; // �X�V����q�I�u�W�F�N�g����

    public void UpdateChildState(bool newState)
    {
        foreach (GameObject child in childObjects)
        {
            if (child != null)
            {
                child.SetActive(newState);
            }
        }
    }
}
