using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private CharacterController2D m_PlayerController;
    private Vector2 playerMovement;

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

    void OnMove(InputValue inputValue)
    {
        playerMovement = inputValue.Get<Vector2>();
    }
}
