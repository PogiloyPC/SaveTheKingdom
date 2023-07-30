using UnityEngine;

public class Wizard : UnitCitizenWarrion
{
    [SerializeField] private Bullet _bullet;

    protected override void StartCitizenUnit()
    {
        InitState(new GuardState(this, Speed, GetComponent<Animator>(), transform), new MoveState(this, GetComponent<Animator>()),
            new StraightAttackState(transform, GetComponent<Animator>(), LayerEnemy, Speed, RadiusCircleEnemy, DistanceAttack, _bullet));
    }
}
