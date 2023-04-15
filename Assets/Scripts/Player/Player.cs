using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController2D m_PlayerController;

    private void Awake()
    {
        m_PlayerController = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        
    }
}
