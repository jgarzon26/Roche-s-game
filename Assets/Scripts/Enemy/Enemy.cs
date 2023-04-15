using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected float _enemySeeRange = 20f;
    protected GameObject m_Player;

    [SerializeField]
    protected int _maxHealth = 5;
    protected int m_Health;

    [SerializeField]
    protected Slider healthBar;


    protected void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Health = _maxHealth;
        healthBar.maxValue = _maxHealth;
        m_Health = _maxHealth;
        healthBar.value = _maxHealth;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _enemySeeRange);
    }

    public void OnHit(int dmg)
    {
        if(m_Health - 1  > 0)
        {
            m_Health -= dmg;
            healthBar.value = m_Health;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
