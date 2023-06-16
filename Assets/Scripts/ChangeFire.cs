using UnityEngine;

public class ChangeFire : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    [SerializeField] private AnimationCurve _fireDamping;

    [SerializeField] private Light _lightFire;    

    [SerializeField] private float _timeDamping;
    private float _maxTimeDamping = 1f;
    private float _currentDamping;
    private float _intensity;

    private void Start()
    {
        _intensity = _lightFire.intensity;
    }

    void Update()
    {
        _currentDamping += Time.deltaTime / _timeDamping;

        _lightFire.intensity = _intensity + _fireDamping.Evaluate(_currentDamping);

        if (_currentDamping >= _maxTimeDamping)
            _currentDamping -= _maxTimeDamping;
    }

    public void OnBurn()
    {
        _anim.SetBool("burned", true);
        _lightFire.enabled = true;
    }

    public void OnDisableBurn()
    {
        _anim.SetBool("burned", false);
        _lightFire.enabled = false;
    }
}
