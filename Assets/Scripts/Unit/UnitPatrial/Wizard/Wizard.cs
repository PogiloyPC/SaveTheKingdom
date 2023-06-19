using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : UnitCitizen
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
