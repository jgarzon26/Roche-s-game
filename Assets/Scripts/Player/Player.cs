using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable
{
    private CharacterController2D m_PlayerController;
    private SpriteRenderer m_PlayerRenderer;

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

    public UnityEvent deathEvent;


    private void Start()
    {
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
        m_PlayerRenderer = GetComponent<SpriteRenderer>();
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
        m_Direction = Input.GetAxisRaw("Horizontal") * _speed;

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

    public void OnHit(int dmg)
    {
        m_Health -= dmg;
        UIManager.Instance.SetCurrentHealth(m_Health);
        /*if (m_Health - 1 > 0)
        {
            m_Health -= dmg;
            UIManager.Instance.SetCurrentHealth(m_Health);
        }
        else
        {
            deathEvent.Invoke();
            Destroy(gameObject);
        }*/
        if (m_Health <= 0)
        {
            deathEvent.Invoke();
            //Destroy(gameObject);
        }
    }
}
