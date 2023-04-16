using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private CharacterController2D m_PlayerController;
    private Animator m_PlayerAnimator;

    /*Movement*/
    [Header("Movement")]
    [SerializeField]
    private float _speed = 5;
    private float m_Direction = 0;
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

    /*Resources*/
    [Header("Resources")]
    [SerializeField]
    private int m_MaxHealth = 10;
    private int m_Health;
    [SerializeField]
    private int m_MaxMana = 100;
    private int m_Mana;


    private void Start()
    {
        const int barrierLayer = 8;
        Physics2D.IgnoreLayerCollision(gameObject.layer, barrierLayer);

        UIManager.Instance.SetMaxHealth(m_MaxHealth);
        m_Health = m_MaxHealth;
        UIManager.Instance.SetCurrentHealth(m_Health);

        UIManager.Instance.SetMaxMana(m_MaxMana);
        m_Mana = m_MaxMana;
        UIManager.Instance.SetCurrentHealth(m_Mana);
    }

    private void Awake()
    {
        m_PlayerController = GetComponent<CharacterController2D>();
        m_PlayerAnimator = GetComponent<Animator>();
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
        m_PlayerAnimator.SetBool("IsMoving", m_Direction > 0 || m_Direction < 0);
    }

    private void ControlPlayer()
    {
        
        m_Direction = Input.GetAxisRaw("Horizontal") * _speed;

        if (Input.GetButtonDown("Jump"))
        {
            m_HasJumped = true;
            m_PlayerAnimator.SetTrigger("HasJumped");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_HasCrouched = true;
            m_PlayerAnimator.SetTrigger("HasCrouch");
            m_PlayerAnimator.SetBool("HasCrouched", true);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            m_HasCrouched = false;
            m_PlayerAnimator.SetBool("HasCrouched", false);
        }
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > m_FireDelay)
        {
            m_FireDelay = Time.time + _fireCooldown;
            m_PlayerAnimator.SetTrigger("OnShoot");
            Projectile projectile = Instantiate(_gunProjectilePrefab, transform.position + _spawnOffset, Quaternion.identity).GetComponent<Projectile>();
            projectile.DirectionToShoot = (m_PlayerController.FacingRight) ? Direction.right : Direction.left;
        }
    }

    public void OnHit(int dmg)
    {
        if (m_Health - 1 > 0)
        {
            m_Health = m_Health - dmg;
            UIManager.Instance.SetCurrentHealth(m_Health);
        }
        else
        {
            //Destroy(gameObject);
        }
    }
}
