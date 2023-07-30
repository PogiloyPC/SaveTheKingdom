using System.Collections;
using UnityEngine;
using InterfaceTask;

public class Fish : MonoBehaviour
{
    [SerializeField] private MoneyPlayer _money;

    [SerializeField] private AnimationCurve _flyCurve;

    private float _finishFlyTime;

    private Vector3 _startPos;

    public void InitFish(IFieldForFish field)
    {
        _startPos = field.MyPos();

        transform.position = field.MyPos();

        _finishFlyTime = field.FinishValue();
    }

    public void PullFish(IFieldForFish field)
    {
        gameObject.SetActive(field.IsPulled());

        if (gameObject.activeSelf)
            StartCoroutine(FlyFish());
    }

    private IEnumerator FlyFish()
    {
        float _currentTimeFly = 0f;

        Vector3 pos = transform.position;

        while (true)
        {
            transform.position = new Vector3(pos.x, pos.y + _flyCurve.Evaluate(_currentTimeFly));

            _currentTimeFly += Time.deltaTime;

            if (_currentTimeFly >= _finishFlyTime)
            {
                Instantiate(_money, transform.position, Quaternion.identity);

                gameObject.SetActive(false);

                transform.position = _startPos;

                break;
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
