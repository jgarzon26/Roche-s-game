using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private Vector3 _spawnOffset;
    [SerializeField]
    private float _fireCooldown = 1;
    private float m_FireDelay = 0;

    [SerializeField]
    private float _enemySeeRange = 20f;
    private GameObject m_Player;


    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        ShootPlayerIfNear();
    }

    private void ShootPlayerIfNear()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, m_Player.transform.position);
        if(_enemySeeRange >= distanceToPlayer && Time.time > m_FireDelay)
        {
            m_FireDelay = Time.time + _fireCooldown;
            Projectile projectile = Instantiate(_bulletPrefab, transform.position + _spawnOffset, Quaternion.identity).GetComponent<Projectile>();
            projectile.PlayerLastPosition = m_Player.transform.position - transform.position;
            AudioManager.Instance.PlayCombat();
        }
        else if(_enemySeeRange < distanceToPlayer)
        {
            AudioManager.Instance.PlayExplore();
        }
     
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _enemySeeRange);
    }
}
