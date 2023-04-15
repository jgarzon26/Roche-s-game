using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float m_BulletLifetime = 1f;

    void Start()
    {
        Destroy(gameObject, m_BulletLifetime);
    }

}
