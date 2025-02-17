using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MM_PlayerJoinManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInput.Instantiate(player);
    }

    public void InstantiateCharacter()
    {
        PlayerInput.Instantiate(player);
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("�Q��");
        foreach (var device in playerInput.devices)
        {
            Debug.Log("����f�o�C�X" + device);
        }
    }
}
