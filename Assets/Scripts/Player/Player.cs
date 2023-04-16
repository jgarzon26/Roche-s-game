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

    /*Flying*/
    [Header("Flying")]
    public bool canFly = false;
    private bool isFlying = false;
    [SerializeField]
    private float manaCost = 30f;

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
    private float m_MaxMana = 100;
    private float m_Mana;
    [SerializeField]
    private float manaRegen = 3f;
    public bool isCasting = false;

    public UnityEvent deathEvent;


    private void Start()
    {
        UIManager.Instance.SetMaxHealth(m_MaxHealth);
        m_Health = m_MaxHealth;
        UIManager.Instance.SetCurrentHealth(m_Health);

        UIManager.Instance.SetMaxMana(m_MaxMana);
        m_Mana = m_MaxMana;
        UIManager.Instance.SetCurrentMana(m_Mana);
    }

    private void Awake()
    {
        m_PlayerController = GetComponent<CharacterController2D>();
        m_PlayerRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Debug.Log(m_Mana);
        UIManager.Instance.SetCurrentMana(m_Mana);
        if (m_Mana < m_MaxMana && !isCasting)
            m_Mana += Time.deltaTime * manaRegen;
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
        if (isFlying)
        {
            m_Mana -= Time.deltaTime * manaCost;
            m_PlayerController.Fly();
        }
            
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

        if (Input.GetButtonDown("Fly") && canFly)
        {
            if (m_Mana > 0)
            {
                isCasting = true;
                isFlying = true;               
            }
            
        }
        else if (Input.GetButtonUp("Fly") || m_Mana <= 0f)
        {
            isCasting = false;
            isFlying = false;
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
