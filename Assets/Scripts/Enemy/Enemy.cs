using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected float _enemySeeRange = 20f;
    protected GameObject m_Player;

    [SerializeField]
    protected int _maxHealth = 5;
    protected int m_Health;


    protected void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Health = _maxHealth;
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
            m_Health = m_Health - dmg;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
