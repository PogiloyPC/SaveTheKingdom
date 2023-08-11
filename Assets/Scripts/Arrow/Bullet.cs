using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IHitEnemy
{
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField, Range(0, 10)] private float _speed;
    [SerializeField] private float _damage;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void Shot(IDirectionShot direction)
    {
        _rb.AddForce(direction.DirectionShot() * _speed, ForceMode2D.Impulse);
    }

    public float Hit() => _damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IEnemyHealth enemy = other.gameObject.GetComponent<IEnemyHealth>();

        if (enemy != null)
            enemy.TakeDamage(this);
    }
}
