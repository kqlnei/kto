using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MM_SetOnClick_PauseButton : MonoBehaviour
{
    [SerializeField]
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(MM_TimeManager.Instance.MoveTime);
        button.onClick.AddListener(()=>Destroy(this.gameObject));
        button.onClick.AddListener(()=>MM_PlayerStateManager.Instance.SetPlayerState(MM_PlayerStateManager.PlayerState.Playing));
    }
}
