using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : Enemy
{
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private Vector3 _spawnOffset;
    [SerializeField]
    private float _fireCooldown = 1;
    private float m_FireDelay = 0;

    private void Update()
    {
        ShootPlayerIfNear();
    }

    private void ShootPlayerIfNear()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, m_Player.transform.position);
        if (_enemySeeRange >= distanceToPlayer && Time.time > m_FireDelay)
        {
            m_FireDelay = Time.time + _fireCooldown;
            Projectile projectile = Instantiate(_bulletPrefab, transform.position + _spawnOffset, Quaternion.identity).GetComponent<Projectile>();
            projectile.PlayerLastPosition = m_Player.transform.position - transform.position;
            //AudioManager.Instance.PlayCombat();
        }
        // else if (_enemySeeRange < distanceToPlayer)
        // {
        //     AudioManager.Instance.PlayExplore();
        // }

    }
}
