using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private Transform _playerPos;

    [SerializeField] private Vector3 offset;

    [SerializeField] private float _damping;

    private void LateUpdate()
    {
        Vector3 posPlayer = new Vector3(_playerPos.position.x + (offset.x * _playerPos.localScale.x), _playerPos.position.y + offset.y, offset.z);
        Vector3 pos = Vector3.Lerp(transform.position, posPlayer, _damping * Time.deltaTime);

        transform.position = new Vector3(pos.x, pos.y, pos.z);
    }
}
