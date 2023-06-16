using UnityEngine;

public class Carpenter : UnitCitizen
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
