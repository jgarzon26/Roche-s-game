using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Enemy
{
    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private Transform[] _wayPoints;
    private int m_CurrentWaypoint = 0;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, m_Player.transform.position);
        float movement = _speed * Time.deltaTime;
        const float offsetDistance = 0.05f;

        if(_enemySeeRange < distanceToPlayer)
        {
            if(m_CurrentWaypoint < _wayPoints.Length)
            {
                transform.position = Vector2.MoveTowards(transform.position, _wayPoints[m_CurrentWaypoint].position, movement);
                if(Vector2.Distance(transform.position, _wayPoints[m_CurrentWaypoint].position) < offsetDistance)
                {
                    m_CurrentWaypoint++;
                }
            }
            else
            {
                m_CurrentWaypoint = 0;
            }
        }
    }
}
