using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // [SerializeField]
    // private float _followSpeed = 5f;
    // [SerializeField]
    // private float xOffset = -1.2f;
    // [SerializeField]
    // private float yOffset = 3.39f;
    // [SerializeField]
    // private float zOffset = -39.28f;
    // [SerializeField]
    // private Transform _playerTransform;

    // private void Update()
    // {
    //     Vector3 newPos = new(_playerTransform.position.x + xOffset, _playerTransform.position.y + yOffset, zOffset);
    //     transform.position = Vector3.Slerp(transform.position, newPos, _followSpeed * Time.deltaTime);
    // }

    [SerializeField]
    private Transform targetToFollow;

    private void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, -11.2f, 659f),
            Mathf.Clamp(targetToFollow.position.y, 2.72f, 21f),
            transform.position.z);

    }
    
}
