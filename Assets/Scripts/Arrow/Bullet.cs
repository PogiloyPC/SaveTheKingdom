using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Range(0, 6)] private float _speed;

    [SerializeField] private Rigidbody2D _rb;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void Shot(IDirectionShot direction)
    {
        _rb.AddForce(direction.DirectionShot() * _speed, ForceMode2D.Impulse);
    }   
}
