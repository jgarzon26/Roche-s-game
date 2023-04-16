using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { left, right }

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected float _projectileSpeed = 10f;
    [SerializeField]
    protected float _duration = 20f;


    public Direction DirectionToShoot { private get; set; }

    public Vector2 PlayerLastPosition { get; set; }


    protected virtual void Start()
    {
        Destroy(gameObject, _duration);
    }

    protected void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        const int playerLayer = 3;
        Vector2 direction;
        if(gameObject.layer == playerLayer) direction = (DirectionToShoot.Equals(Direction.right)) ? Vector2.right : Vector2.left;
        else direction = PlayerLastPosition.normalized;

        Vector2 movement =  _projectileSpeed * Time.deltaTime * direction;
        transform.Translate(movement);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.layer != other.transform.gameObject.layer)
        {
            other.GetComponent<IDamageable>().OnHit(1);
            Destroy(gameObject);
        }
    }



}
