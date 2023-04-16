using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShotProjectile : Projectile
{
    [SerializeField]
    private Sprite _fullChargeSprite;
    private SpriteRenderer m_Renderer;

    private Animator m_Animator;

    [SerializeField]
    private int _chargedShotDmg = 5;

    private bool m_CanShootFlag = false;

    private void Awake()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();

    }

    protected override void Move()
    {
        if(m_CanShootFlag)
        {
            base.Move();
        }
    }

    public void ReplaceFullCharge()
    {
        m_Renderer.sprite = _fullChargeSprite;
        m_CanShootFlag = true;
        
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.layer != other.transform.gameObject.layer)
        {
            m_Animator.SetBool("HasHit", true);
            other.GetComponent<IDamageable>().OnHit(_chargedShotDmg);
        }
    }

    public void DestroyOnColllide()
    {
        Destroy(gameObject);
    }
}
