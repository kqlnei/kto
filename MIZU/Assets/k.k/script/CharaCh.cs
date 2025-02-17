using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChararCh : MonoBehaviour
{
    public bool Mode;

    GameObject[] characters;
    GameObject currentChar;
    Vector3[] initialPositions; // �e�L�����N�^�[�̏����ʒu��ێ�

    int _currentCharNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        // ���̃I�u�W�F�N�g�̎q�I�u�W�F�N�g�����ׂĎ擾
        List<GameObject> characterList = new List<GameObject>();
        List<Vector3> initialPositionList = new List<Vector3>(); // �����ʒu��ۑ����郊�X�g

        foreach (Transform child in transform)
        {
            characterList.Add(child.gameObject);
            initialPositionList.Add(child.position); // �����ʒu��ۑ�
        }
        characters = characterList.ToArray();
        initialPositions = initialPositionList.ToArray();

        Debug.Log("I have " + characters.Length + " Changable Characters");

        ChangeCharacter(_currentCharNum);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mode) // Player 1
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (_currentCharNum == characters.Length - 1)
                {
                    _currentCharNum = 0;
                }
                else
                {
                    _currentCharNum++;
                }
                Debug.Log("Character " + _currentCharNum + " Selected");
                ChangeCharacter(_currentCharNum);
            }
        }
        else // Player 2
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (_currentCharNum == characters.Length - 1)
                {
                    _currentCharNum = 0;
                }
                else
                {
                    _currentCharNum++;
                }
                Debug.Log("Character " + _currentCharNum + " Selected");
                ChangeCharacter(_currentCharNum);
            }
        }
    }

    void ChangeCharacter(int characterNum)
    {
        // ���݂̃L�����N�^�[�̈ʒu�Ɖ�]��ۑ�����
        Vector3 currentPos = Vector3.zero;
        Quaternion currentRot = Quaternion.identity;
        if (currentChar != null)
        {
            currentPos = currentChar.transform.position;
            currentRot = currentChar.transform.rotation;
        }

        // ��������S�L�����N�^�[���A�N�e�B�u�ɂ���
        foreach (GameObject gObj in characters)
        {
            gObj.SetActive(false);
        }

        // �V�����L�����N�^�[���A�N�e�B�u�ɂ���
        currentChar = characters[characterNum];
        currentChar.SetActive(true);

        // �ۑ������ʒu�Ɖ�]��V�����L�����N�^�[�ɓK�p����
        if (currentPos != Vector3.zero)
        {
            currentChar.transform.position = currentPos;
            currentChar.transform.rotation = currentRot;
        }
        else
        {
            // �����ʒu��K�p
            currentChar.transform.position = initialPositions[characterNum];
        }
    }
}
