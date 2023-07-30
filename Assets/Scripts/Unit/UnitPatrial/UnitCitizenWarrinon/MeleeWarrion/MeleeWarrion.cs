using UnityEngine;

public class MeleeWarrion : UnitCitizenWarrion
{
    protected override void StartCitizenUnit()
    {
        InitState(new GuardState(this, Speed, GetComponent<Animator>(), transform), new MoveState(this, GetComponent<Animator>()),
            new StateAttackMelee(transform, GetComponent<Animator>(), LayerEnemy, Speed, RadiusCircleEnemy, DistanceAttack));
    }
}
