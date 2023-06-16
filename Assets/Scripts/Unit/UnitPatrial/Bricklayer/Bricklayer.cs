using UnityEngine;

public class Bricklayer : UnitCitizen
{
    protected override void StartCitizenUnit()
    {
        InitState(new FollowObjectState(this, GetComponent<Animator>()), new MoveState(this, GetComponent<Animator>()));
    }

    private void Update()
    {
        GetStateMachineUnit().Update();
    }
}
