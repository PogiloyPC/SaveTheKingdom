using InterfaceTask;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Millet : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    [SerializeField] private MoneyPlayer _money;

    [SerializeField] private Sprite _1;
    [SerializeField] private Sprite _2;
    [SerializeField] private Sprite _3;
    [SerializeField] private Sprite _4;
    [SerializeField] private Sprite _5;

    [SerializeField] private int _countMoney;

    public void WeedMillet(IFieldForMillet field)
    {
        if (field.CurrentValue() >= field.FinishValue())
        {
            StartCoroutine(Harvest());
            
            _renderer.sprite = _5;
        }
        else if (field.CurrentValue() >= field.FinishValue() * 4f / 5f)
        {
            _renderer.sprite = _4;
        }
        else if (field.CurrentValue() >= field.FinishValue() * 3f / 5f)
        {
            _renderer.sprite = _3;
        }
        else if (field.CurrentValue() >= field.FinishValue() * 2f / 5f)
        {
            _renderer.sprite = _2;
        }
        else if (field.CurrentValue() >= field.FinishValue() * 1f / 5f)
        {
            _renderer.sprite = _1;
        }
        else if (field.CurrentValue() < field.FinishValue() * 1f / 5f)
        {
            _renderer.sprite = null;
        }       
        
        transform.position = field.MyPos();
    }

    private IEnumerator Harvest()
    {
        int currentMoney = 0;

        float posX;

        while (currentMoney < _countMoney)
        {
            posX = Random.Range(-0.2f, 0.2f);

            Rigidbody2D rb = Instantiate(_money, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(posX, transform.up.y) * 4f, ForceMode2D.Impulse);

            currentMoney++;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
