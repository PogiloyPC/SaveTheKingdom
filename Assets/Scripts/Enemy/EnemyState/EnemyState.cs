using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public abstract void EnterState();    

    public abstract void ExitState();   

    public abstract void PlayState();
}
