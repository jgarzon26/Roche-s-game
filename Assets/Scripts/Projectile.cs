using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { left, right }

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _projectileSpeed = 10f;
    [SerializeField]
    private float _duration = 20f;

    public Direction DirectionToShoot { private get; set; }

    public Vector2 PlayerLastPosition { get; set; }

    private void Start()
    {
        Destroy(gameObject, _duration);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        const int playerLayer = 3;
        Vector2 direction;
        if(gameObject.layer == playerLayer) direction = (DirectionToShoot.Equals(Direction.right)) ? Vector2.right : Vector2.left;
        else direction = PlayerLastPosition.normalized;

        Vector2 movement =  _projectileSpeed * Time.deltaTime * direction;
        transform.Translate(movement);
    }

}
