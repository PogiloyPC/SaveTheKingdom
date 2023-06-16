using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    private Transform _posGroundPoint;

    private float _radiusCircle;

    private LayerMask _groundLayer;

    public PlayerState(Transform posGroundPoint, float radius, LayerMask layerMask)
    {
        _posGroundPoint = posGroundPoint;

        _radiusCircle = radius;

        _groundLayer = layerMask;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_posGroundPoint.position, _radiusCircle, _groundLayer);
    }

    public bool IsRunning()
    {
        return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A);
    }
}
