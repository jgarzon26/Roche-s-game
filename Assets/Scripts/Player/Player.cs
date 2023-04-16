using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField]
    private GameObject _chargedShotPrefab;
    [SerializeField]
    private Vector3 _spawnOffsetOfCharge;
    [SerializeField]
    private float _Cooldown = 20;
    private bool m_CanChargeShot = true;

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
        const int barrierLayer = 8;
        Physics2D.IgnoreLayerCollision(gameObject.layer, barrierLayer);

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
        m_PlayerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        UIManager.Instance.SetCurrentMana(m_Mana);
        if (m_Mana < m_MaxMana && !isCasting)
            m_Mana += Time.deltaTime * manaRegen;
        if(!GameManager.Instance.IsPlayerChargingShot) ControlPlayer();
        if(!GameManager.Instance.IsPlayerChargingShot) Shoot();
    }

    private void FixedUpdate()
    {
        if(!m_HasCrouched && !GameManager.Instance.IsPlayerChargingShot)
            MovePlayer();
    }

    private bool HasMoved() => (m_Direction > 0 || m_Direction < 0);

    private void MovePlayer()
    {
        m_PlayerController.Move(m_Direction * Time.fixedDeltaTime, m_HasCrouched, m_HasJumped);
        m_HasJumped = false;
        if (isFlying)
        {
            //m_Mana -= Time.deltaTime * manaCost;
            m_PlayerController.Fly();
        }
        
        m_PlayerAnimator.SetBool("IsMoving", HasMoved());
        if(HasMoved() && m_PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("shoot_anim"))
        {
            m_PlayerAnimator.Play("run_anim");
        }
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

        if (Input.GetButtonDown("Fly") && canFly)
        {
            if (m_Mana > 0)
            {
                isCasting = true;
                isFlying = true;
                m_PlayerAnimator.SetBool("HasFly", true);
                m_PlayerAnimator.SetTrigger("OnFly");
            }
            
        }
        else if (Input.GetButtonUp("Fly") || m_Mana <= 0f)
        {
            isCasting = false;
            isFlying = false;
            m_PlayerAnimator.SetBool("HasFly", false);
        }
    }

    private void Shoot()
    {
        if (HasMoved() == false && isFlying == false)
        {
            if(Input.GetButtonDown("Fire1") && Time.time > m_FireDelay)
            {
                m_FireDelay = Time.time + _fireCooldown;
                m_PlayerAnimator.SetTrigger("OnShoot");
                Projectile projectile = Instantiate(_gunProjectilePrefab, transform.position + _spawnOffset, Quaternion.identity).GetComponent<Projectile>();
                projectile.DirectionToShoot = (m_PlayerController.FacingRight) ? Direction.right : Direction.left;
            }

            if(Input.GetButtonDown("Fire2") && m_CanChargeShot)
            {
                m_CanChargeShot = false;
                GameManager.Instance.IsPlayerChargingShot = true;
                m_PlayerAnimator.SetBool("IsShootCharge", true);
                Invoke(nameof(SpawnBall), 2);
            }
        }
    }

    private void SpawnBall()
    {
        Projectile projectile = Instantiate(_chargedShotPrefab, transform.position + _spawnOffsetOfCharge, Quaternion.identity).GetComponent<Projectile>();
        projectile.DirectionToShoot = (m_PlayerController.FacingRight) ? Direction.right : Direction.left;
    }

    public void ShootChargedShot()
    {
        StartCoroutine(ChargeShotCooldownRoutine());
        m_PlayerAnimator.SetBool("IsShootCharge", false);
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
            m_PlayerAnimator.SetTrigger("OnDeath");
            deathEvent.Invoke();
            //Destroy(gameObject);
        }
    }

    private IEnumerator ChargeShotCooldownRoutine()
    {
        yield return new WaitForSeconds(_Cooldown);
        GameManager.Instance.IsPlayerChargingShot = false;
        m_CanChargeShot = true;
        m_PlayerAnimator.Play("Idle_anim");
    }
}
