using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ShootingEnemyController : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed = 2f;
    [SerializeField] private float m_AttackCooldown = 1f;
    [SerializeField] private float m_Range = 20f;
    public Transform m_Target;
    [SerializeField] private Transform m_Muzzle;
    [SerializeField] private GameObject m_Bullet;
    [SerializeField] private float m_BulletSpeed = 10f;

    private float m_CurrentCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        //attack cooldown
        if (m_CurrentCooldown < m_AttackCooldown)
            m_CurrentCooldown += Time.deltaTime;

        if (Vector2.Distance(transform.position, m_Target.position) <= m_Range)
            Attack();
    }

    void Attack()
    {
        //direction vector
        Vector2 directionToTarget = m_Target.position - transform.position;
        directionToTarget.Normalize();

        if (m_CurrentCooldown >= m_AttackCooldown)
        { 
            GameObject currentBullet = Instantiate(m_Bullet, m_Muzzle.position, Quaternion.identity);
            //apply velocity
            currentBullet.GetComponent<Rigidbody2D>().velocity = directionToTarget * m_BulletSpeed;
            m_CurrentCooldown = 0f;
        }
    }

    private void FixedUpdate()
    {

    }
        
}
