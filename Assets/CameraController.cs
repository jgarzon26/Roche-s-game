using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float _followSpeed = 2f;
    [SerializeField]
    private float yOffset = 1f;
    [SerializeField]
    private float zOffset = -10f;
    [SerializeField]
    private Transform _playerTransform;

    private void Update()
    {
        Vector3 newPos = new(_playerTransform.position.x, _playerTransform.position.y + yOffset, zOffset);
        transform.position = Vector3.Slerp(transform.position, newPos, _followSpeed * Time.deltaTime);
    }
}
