using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController2D m_PlayerController;

    [SerializeField]
    private float _speed = 5;
    private float m_Direction;
    private bool m_HasJumped = false;
    private bool m_HasCrouched = false;

    private void Awake()
    {
        m_PlayerController = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        m_Direction = Input.GetAxis("Horizontal") * _speed;

        if(Input.GetButtonDown("Jump"))
        {
            m_HasJumped = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_HasCrouched = true;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            m_HasCrouched = false;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        m_PlayerController.Move(m_Direction * Time.fixedDeltaTime, m_HasCrouched, m_HasJumped);
        m_HasJumped = false;
    }
}
