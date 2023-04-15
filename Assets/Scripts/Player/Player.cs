using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController2D m_PlayerController;
    [SerializeField] private float m_RunSpeed = 50f;
    private float m_HorizontalMove = 0f;
    private bool m_IsJumping = false;

    private void Awake()
    {
        m_PlayerController = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        m_HorizontalMove = Input.GetAxisRaw("Horizontal") * m_RunSpeed;
        
        if(Input.GetButtonDown("Jump"))
        {
            m_IsJumping = true;
        }
    }

    private void FixedUpdate()
    {
        m_PlayerController.Move(m_HorizontalMove * Time.fixedDeltaTime, false, m_IsJumping);
        m_IsJumping=false;
    }
}
