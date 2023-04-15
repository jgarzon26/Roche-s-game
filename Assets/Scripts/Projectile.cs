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

    public Direction DirectionOfPlayer { private get; set; } = Direction.right;

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
        Vector2 direction = (DirectionOfPlayer.Equals(Direction.right)) ? Vector2.right : Vector2.left;
        Vector2 movement =  _projectileSpeed * Time.deltaTime * direction;
        transform.Translate(movement);
    }
}
