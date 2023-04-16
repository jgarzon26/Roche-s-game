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

    private SpriteRenderer m_SpriteRenderer;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
       
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, m_Player.transform.position);
        float movement = _speed * Time.deltaTime;
        const float offsetDistance = 0.05f;
        Vector2 targetPosition = Vector2.zero;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, _enemySeeRange, _LayerMask);

        //_enemySeeRange < distanceToPlayer
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            targetPosition = m_Player.transform.position;
            Vector2 direction = targetPosition - (Vector2)transform.position;
            float distance = Vector2.Distance(transform.position, targetPosition);
            const float offsetDistanceToPlayer = 4f;

            if (distance > offsetDistanceToPlayer)
            {
                Vector2 move = direction.normalized * Mathf.Min(distance - offsetDistanceToPlayer, movement);
                transform.position += (Vector3)move;
            }
            AudioManager.Instance.PlayCombat();

            
        }
        else
        {
            AudioManager.Instance.PlayExplore();
            if (m_CurrentWaypoint < _wayPoints.Length)
            {
                targetPosition = _wayPoints[m_CurrentWaypoint].position;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, movement);
                if (Vector2.Distance(transform.position, targetPosition) < offsetDistance)
                {
                    m_CurrentWaypoint++;
                }
            }
            else
            {
                m_CurrentWaypoint = 0;
            }
        }

        if(transform.position.x > targetPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); 
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
