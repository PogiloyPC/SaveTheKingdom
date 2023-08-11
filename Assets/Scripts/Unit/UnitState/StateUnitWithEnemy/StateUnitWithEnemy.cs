using UnityEngine;

public abstract class StateUnitWithEnemy : UnitState
{
    private Transform _transformUnit;

    internal IEnemyHealth _enemy;

    public LayerMask EnemyLayer { get; private set; }

    public float Speed { get; private set; }
    public float RadiusCircle { get; private set; }
    public float TimerAghast;
    public float StartTimerAghast { get; private set; } = 5f;

    public StateUnitWithEnemy(Transform transformUnit, Animator anim, LayerMask enemyLayer, float speed, float radiusCircle)
    {
        _transformUnit = transformUnit;

        EnemyLayer = enemyLayer;

        Speed = speed;
        RadiusCircle = radiusCircle;
    }

    public bool EnemyFixed()
    {
        if (_enemy != null)
            return true;
        else
            return false;

    }

    protected Transform PosUnit() => _transformUnit;

    protected void LookRotationEnemy()
    {
        if (_transformUnit.position.x > _enemy.PosTarget().x)
            _transformUnit.localScale = new Vector3(-1f, 1f, 1f);
        else
            _transformUnit.localScale = new Vector3(1f, 1f, 1f);
    }

    protected void LookEnemy()
    {
        Collider2D collider = Physics2D.OverlapCircle(_transformUnit.position, RadiusCircle, EnemyLayer);

        _enemy = collider?.gameObject.GetComponent<IEnemyHealth>();
    }

    public bool Aghast() => TimerAghast > 0f;
}
