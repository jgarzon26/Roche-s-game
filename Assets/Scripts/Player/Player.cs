using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController2D m_PlayerController;

    /*Movement*/
    [Header("Movement")]
    [SerializeField]
    private float _speed = 5;
    private float m_Direction;
    private bool m_HasJumped = false;
    private bool m_HasCrouched = false;

    /*Projectile*/
    [Header("Projectile")]
    [SerializeField]
    private GameObject _gunProjectilePrefab;
    [SerializeField]
    private Vector3 _spawnOffset;
    [SerializeField]
    private float _fireCooldown = 2;
    private float m_FireDelay = 0;


    private void Awake()
    {
        m_PlayerController = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        ControlPlayer();
        Shoot();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        m_PlayerController.Move(m_Direction * Time.fixedDeltaTime, m_HasCrouched, m_HasJumped);
        m_HasJumped = false;
    }

    private void ControlPlayer()
    {
        m_Direction = Input.GetAxis("Horizontal") * _speed;

        if (Input.GetButtonDown("Jump"))
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

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > m_FireDelay)
        {
            m_FireDelay = Time.time + _fireCooldown;
            Projectile projectile = Instantiate(_gunProjectilePrefab, transform.position + _spawnOffset, Quaternion.identity).GetComponent<Projectile>();
            projectile.DirectionToShoot = Direction.right;
        }
    }
}
