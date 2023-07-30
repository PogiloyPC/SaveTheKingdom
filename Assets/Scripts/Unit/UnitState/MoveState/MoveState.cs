using System.Collections;
using UnityEngine;

public class MoveState : UnitState
{
    private Unit _unit;

    private Animator _anim;

    private Vector3 _movePosition;

    private bool _isChilling;

    private float _maxChillTime = 2.5f;
    private float _minChillTime = 1f;    

    public MoveState(Unit unit, Animator anim)
    {
        _unit = unit;
        _anim = anim;        

        FindPointMove(_unit.LeftBorders().x, _unit.RightBorders().x);
    }

    public override void EnterState()
    {
        CheckSide(_movePosition.x);
    }

    public override void PlayState()
    {
        MoveUnit();
    }

    public override void ExitState()
    {
        
    }

    private void MoveUnit()
    {
        if (_unit.transform.position.x != _movePosition.x && !_isChilling)
            Wander();
        else if (_isChilling == true)
            _unit.StartCoroutine(Chill(_unit.LeftBorders().x, _unit.RightBorders().x));
    }

    private void Wander()
    {
        _unit.transform.position = Vector3.MoveTowards(_unit.transform.position, _movePosition, _unit.Speed * Time.deltaTime);       

        _anim.SetBool("run", true);

        if (_unit.transform.position.x == _movePosition.x)
        {
            _isChilling = true;

            _anim.SetBool("run", false);
        }
    }

    private void CheckSide(float x)
    {
        if (x > _unit.transform.position.x)
            _unit.transform.localScale = new Vector3(1f, 1f, 1f);
        else
            _unit.transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    private void FindPointMove(float min, float max)
    {
        float posX = Random.Range(min, max);

        _movePosition = new Vector3(posX, _unit.transform.position.y, 0);        

        CheckSide(_movePosition.x);
    }

    private IEnumerator Chill(float min, float max)
    {
        _isChilling = false;

        int number = Random.Range(1, 4);

        float chillTime = Random.Range(_minChillTime, _maxChillTime);

        _anim.SetBool("idle" + number, true);

        yield return new WaitForSeconds(chillTime);

        _anim.SetBool("idle" + number, false);

        FindPointMove(min, max);
    }
}
