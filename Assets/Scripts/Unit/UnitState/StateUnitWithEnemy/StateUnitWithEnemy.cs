using UnityEngine;

public abstract class StateUnitWithEnemy : UnitState
{
    private Transform _transformUnit;

    private Enemy _enemy;

    public LayerMask EnemyLayer { get; private set; }

    public float Speed { get; private set; }
    public float RadiusCircle { get; private set; }
    public float TimerAghast;
    public float StartTimerAghast { get; private set; } = 5f;

    public Vector3 EnemyPos => _enemy.transform.position;

    public StateUnitWithEnemy(Transform transformUnit, Animator anim, LayerMask enemyLayer, float speed, float radiusCircle)
    {
        _transformUnit = transformUnit;

        EnemyLayer = enemyLayer;

        Speed = speed;
        RadiusCircle = radiusCircle;
    }

    public bool EnemyFixed() => _enemy;

    protected Transform PosUnit() => _transformUnit;

    protected void LookRotationEnemy()
    {
        if (_transformUnit.position.x > _enemy.transform.position.x)
            _transformUnit.localScale = new Vector3(-1f, 1f, 1f);
        else
            _transformUnit.localScale = new Vector3(1f, 1f, 1f);
    }

    protected void LookEnemy()
    {
        Collider2D collider = Physics2D.OverlapCircle(_transformUnit.position, RadiusCircle, EnemyLayer);

        _enemy = collider?.gameObject.GetComponent<Enemy>();
    }

    public bool Aghast() => TimerAghast > 0f;
}
